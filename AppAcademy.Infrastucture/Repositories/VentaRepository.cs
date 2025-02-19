using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.Enum;
using AppAcademy.Domain.PuntoDeVenta;
using AppAcademy.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace AppAcademy.Infrastucture.Repositories
{
    public class VentaRepository : AsyncRepository<Venta>, IVentaRepository
    {
        private readonly IProductoRepository _productoRepository;

        public VentaRepository(AppAcademyDbContext dbContext, IProductoRepository productoRepository) : base(dbContext)
        {
            _productoRepository = productoRepository;
        }

        public async Task<Venta> CreateVenta(Venta venta)
        {
           using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    // Calcular el total de la venta
                    decimal total = venta.DetalleVentas.Sum(d => d.PrecioUnitario * d.Cantidad);
                    decimal totalConDescuento = total - venta.Descuento;
                    decimal impuestoCalculado = totalConDescuento * (venta.Impuesto / 100);
                    decimal totalFinal = totalConDescuento + impuestoCalculado;

                    // Asignar valores calculados a la venta
                    venta.Total = total;
                    venta.SaldoPendiente = totalFinal;
                    venta.EstadoVenta = VentaEstado.Pendiente;
                    venta.Fecha = DateTime.Now;

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
                    await _dbContext.Ventas.AddAsync(venta);
                    await _dbContext.SaveChangesAsync();  // Guardar la venta en la base de datos

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
    
        public async Task<bool> DeleteVenta(string ventaId)
        {
          using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var venta = await _dbContext.Ventas
                        .Include(v => v.DetalleVentas)
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

                    // Guardar los cambios en la base de datos
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

        public async Task<Venta> GetVentaById(string ventaId)
        {
            try
            {
                var venta = await _dbContext.Ventas
                       .Include(v => v.DetalleVentas)
                       .Include(v => v.Abonos)
                       .FirstOrDefaultAsync(v => v.VentaId == ventaId);

                if (venta == null)
                    throw new Exception("Venta no encontrada");

                return venta;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

