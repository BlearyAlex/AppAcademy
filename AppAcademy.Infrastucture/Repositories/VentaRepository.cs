using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Features.Ventas.Queries.GetAllVentas;
using AppAcademy.Application.Features.Ventas.Queries.GetVenta;
using AppAcademy.Domain.Enum;
using AppAcademy.Domain.Logs;
using AppAcademy.Domain.PuntoDeVenta;
using AppAcademy.Infrastucture.Identity;
using AppAcademy.Infrastucture.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AppAcademy.Infrastucture.Repositories
{
    public class VentaRepository : AsyncRepository<Venta>, IVentaRepository
    {
        private readonly IProductoRepository _productoRepository;
        private readonly UserManager<AppUser> _userManager;

        public VentaRepository(AppAcademyDbContext dbContext, IProductoRepository productoRepository, UserManager<AppUser> userManager) : base(dbContext)
        {
            _productoRepository = productoRepository;
            _userManager = userManager;
        }

        public async Task<Venta> CreateVenta(Venta venta, string userName)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    // Calcular el subtotal sumando el precio unitario por la cantidad de cada detalle.
                    decimal subtotal = venta.DetalleVentas.Sum(d => d.PrecioUnitario * d.Cantidad);

                    // Aplicar el descuento como porcentaje.
                    decimal descuentoCalculado = subtotal * (venta.Descuento / 100);
                    decimal totalConDescuento = subtotal - descuentoCalculado;

                    // Calcular el impuesto sobre el total después del descuento.
                    decimal impuestoCalculado = totalConDescuento * (venta.Impuesto / 100);

                    // Total final es la suma del total con descuento más el impuesto.
                    decimal totalFinal = totalConDescuento + impuestoCalculado;

                    // Asignar valores calculados a la venta
                    venta.Total = totalFinal;
                    venta.SaldoPendiente = totalFinal;
                    venta.EstadoVenta = VentaEstado.Pendiente;
                    venta.Fecha = DateTime.UtcNow;
                    venta.Folio = await GenerateFolioAsync();

                    // Descontar el stock de los productos
                    foreach (var detalle in venta.DetalleVentas)
                    {
                        var producto = await _productoRepository.GetById(detalle.ProductoId);
                        if (producto == null || producto.Stock < detalle.Cantidad)
                        {
                            throw new Exception($"Producto no disponible o stock insuficiente para el producto: {detalle.ProductoId}");
                        }
                        await _productoRepository.DescontarStock(detalle.ProductoId, detalle.Cantidad);
                    }

                    // Agregar la venta a la base de datos
                    await AddAsync(venta);

                    var usuario = await _userManager.FindByNameAsync(userName);
                    if (usuario == null) throw new Exception("Usuario no encontrado.");

                    var bitacora = new Bitacora
                    {
                        UsuarioId = usuario.Id,
                        Fecha = DateTime.UtcNow,
                        Descripcion = $"Se registró un nueva venta con Folio: {venta.Folio} por un Total: ${venta.Total}.00 por el Usuario: {usuario.UserName}",
                        ReferenciaId = venta.VentaId.ToString(),
                        TipoReferencia = "Venta Nueva"
                    };

                    _dbContext.Bitacora.Add(bitacora);
                    await _dbContext.SaveChangesAsync(); 

                    // Confirmar la transacción
                    await transaction.CommitAsync();

                    return venta;
                }
                catch (Exception ex)
                {
                    // Si hay un error, revertimos la transacción
                    await transaction.RollbackAsync();
                    throw new Exception("Error al registrar la venta: " + ex.Message, ex);
                }
            }
        }

        public async Task<bool> UpdateVentaSaldo(Venta venta)
        {
            try
            {
                var existingVenta = await _dbContext.Ventas
                    .Include(v => v.DetalleVentas)
                    .FirstOrDefaultAsync(v => v.VentaId == venta.VentaId);

                if (existingVenta == null)
                    return false;

                existingVenta.SaldoPendiente = venta.SaldoPendiente;
                existingVenta.EstadoVenta = venta.EstadoVenta;
                existingVenta.Fecha = venta.Fecha;

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la venta.", ex);
            }
        }

        public async Task<bool> DeleteVenta(string ventaId, string userName)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var venta = await _dbContext.Ventas
                        .Include(v => v.DetalleVentas)
                        .Include(v => v.Abonos)
                        .FirstOrDefaultAsync(v => v.VentaId == ventaId);

                    if (venta == null)
                    {
                        throw new Exception("Venta no encontrada");
                    }

                    // Revertir el stock de los productos vendidos
                    foreach (var detalle in venta.DetalleVentas)
                    {
                        var producto = await _productoRepository.GetById(detalle.ProductoId);
                        if (producto != null)
                        {
                            // Reponer el stock
                            await _productoRepository.AgregarStock(detalle.ProductoId, detalle.Cantidad);
                        }
                    }

                    // Eliminar los detalles de la venta
                    _dbContext.VentaDetalle.RemoveRange(venta.DetalleVentas);

                    // Eliminar la venta
                    _dbContext.Ventas.Remove(venta);

                    var usuario = await _userManager.FindByNameAsync(userName);
                    if (usuario == null) throw new Exception("Usuario no encontrado.");

                    // Mensaje dinamico de bitacora
                    string mensajeBitacora = $"Se eliminó la venta con Folio: {venta.Folio} de un Total: ${venta.Total}.00 por el Usuario: {usuario.UserName}.";
                    if (venta.Abonos.Any())
                    {
                        decimal totalAbonado = venta.Abonos.Sum(a => a.Monto);
                        mensajeBitacora += $" La venta tenía {venta.Abonos.Count} abonos de un Total: ${totalAbonado}.";
                    }

                    var bitacora = new Bitacora
                    {
                        UsuarioId = usuario.Id,
                        Fecha = DateTime.UtcNow,
                        Descripcion = mensajeBitacora,
                        ReferenciaId = venta.VentaId.ToString(),
                        TipoReferencia = "Venta Eliminada"
                    };

                    _dbContext.Bitacora.Add(bitacora);
                    await _dbContext.SaveChangesAsync();

                    // Confirmar la transacción
                    await transaction.CommitAsync();

                    return true;

                }
                catch (Exception ex)
                {
                    // Si ocurre un error, revertir la transacción
                    await transaction.RollbackAsync();
                    throw new Exception("Error al eliminar la venta: " + ex.Message, ex);
                }
            }
        }

        public async Task<GetVentaVm> GetVentaById(string ventaId)

        {
            try
            {
                var venta = await _dbContext.Ventas
                       .Include(v => v.DetalleVentas)
                       .ThenInclude(v => v.Producto)
                       .Include(v => v.Abonos)
                       .FirstOrDefaultAsync(v => v.VentaId == ventaId);

                if (venta == null)
                    throw new Exception("Venta no encontrada");

                return new GetVentaVm
                {
                    VentaId = venta.VentaId,
                    Fecha = venta.Fecha,
                    EstadoVenta = venta.EstadoVenta.ToString(),
                    SaldoPendiente = venta.SaldoPendiente,
                    ClienteId = venta.ClienteId,
                    Descuento = venta.Descuento,
                    Total = venta.Total,
                    Impuesto = venta.Impuesto,
                    Folio = venta.Folio,
                    VentaDetalle = venta.DetalleVentas.Select(d => new GetVentaDetalleVm
                    {
                        VentaDetalleId = d.VentaDetalleId,
                        VentaId = d.Venta.VentaId,
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario,
                        Total = d.Total,
                        Producto = new GetProductoVm
                        {
                            ProductoId = d.Producto.ProductoId,
                            Nombre = d.Producto.Nombre,
                            Imagen = d.Producto.Imagen,
                            Precio = d.Producto.Precio
                        }
                    }).ToList(),
                    Abonos = venta.Abonos
                    .OrderByDescending(a => a.Fecha)
                    .Select(a => new GetAbonoVm
                    {
                        AbonoId = a.AbonoId,
                        VentaId = a.Venta.VentaId,
                        Monto = a.Monto,
                        Fecha = a.Fecha
                    }).ToList()
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<GetAllVentasVm>> GetAllVentas()
        {
            try
            {
                var venta = await _dbContext.Ventas
                     .Include(v => v.Cliente)
                     .ToListAsync();

                var result = venta.Select(a => new GetAllVentasVm
                {
                    VentaId = a.VentaId,
                    Fecha = a.Fecha,
                    EstadoVenta = a.EstadoVenta.ToString(),
                    SaldoPendiente = a.SaldoPendiente,
                    Descuento = a.Descuento,
                    Total = a.Total,
                    Impuesto = a.Impuesto,
                    Folio = a.Folio,
                    Cliente = a.Cliente != null ? new GetAllVentasClient
                    {
                        ClienteId = a.Cliente.ClienteId,
                        Nombre = a.Cliente.Nombre,
                        Apellido = a.Cliente.Apellido,
                        Telefono = a.Cliente.Telefono,
                    } : null
                }).ToList();

                return result;
            }
            catch (Exception)
            {

                throw;
            }


        }

        #region Private Methods
        private async Task<string> GenerateFolioAsync()
        {
            int anio = DateTime.Now.Year;
            var ultimaVenta = await _dbContext.Ventas
                .Where(v => v.Fecha.Year == anio)
                .OrderByDescending(v => v.Folio)
                .FirstOrDefaultAsync();

            int numeroNumero = 1;
            if (ultimaVenta != null && int.TryParse(ultimaVenta.Folio.Split('-').Last(), out int ultimoNumero))
            {
                numeroNumero = ultimoNumero + 1;
            }

            return $"{anio}-{numeroNumero:D4}";
        }
        #endregion
    }
}

