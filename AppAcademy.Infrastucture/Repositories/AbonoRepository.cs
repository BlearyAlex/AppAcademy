using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Features.Abonos.Command.CreateAbono;
using AppAcademy.Domain.ControlVentas;
using AppAcademy.Domain.Enum;
using AppAcademy.Domain.Logs;
using AppAcademy.Infrastucture.Identity;
using AppAcademy.Infrastucture.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AppAcademy.Infrastucture.Repositories
{
    public class AbonoRepository : AsyncRepository<Abono>, IAbonoRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IBitacoraRepository _bitacoraRepository;
        private readonly IReciboAbonoPdfService _reciboPdfService;

        public AbonoRepository(AppAcademyDbContext dbContext, UserManager<AppUser> userManager, IBitacoraRepository bitacoraRepository, IReciboAbonoPdfService reciboAbonoPdfService) : base(dbContext)
        {
            _userManager = userManager;
            _bitacoraRepository = bitacoraRepository;
            _reciboPdfService = reciboAbonoPdfService;
        }

        public async Task<CreateAbonoResult> CreateAbono(string ventaId, decimal montoAbonado, string userName)
        {
            if (montoAbonado <= 0) throw new ArgumentException("El monto abonado debe ser mayor que cero.");

            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var venta = await _dbContext.Ventas
                        .Include(v => v.Cliente)
                        .Include(v => v.DetalleVentas)
                            .ThenInclude(d => d.Producto)
                        .FirstOrDefaultAsync(v => v.VentaId == ventaId);

                    if (venta == null) throw new Exception("Venta no encontrada.");

                    // Calcular cambio antes de modificar el saldo
                    decimal cambio = 0;
                    if (montoAbonado > venta.SaldoPendiente)
                    {
                        cambio = montoAbonado - venta.SaldoPendiente;
                    }

                    var abono = new Abono
                    {
                        VentaId = ventaId,
                        Monto = montoAbonado,
                        Fecha = DateTime.UtcNow
                    };

                    await _dbContext.Abono.AddAsync(abono);
                    await _dbContext.SaveChangesAsync();

                    // Ajustar saldo pendiente
                    if (montoAbonado >= venta.SaldoPendiente)
                    {
                        venta.SaldoPendiente = 0;
                        venta.EstadoVenta = VentaEstado.Pagado;
                    }
                    else
                    {
                        venta.SaldoPendiente -= montoAbonado;
                    }

                    var usuario = await _userManager.FindByNameAsync(userName);
                    if (usuario == null) throw new Exception("Usuario no encontrado");

                    var bitacora = new Bitacora
                    {
                        UsuarioId = usuario.Id,
                        Fecha = DateTime.UtcNow,
                        Descripcion = $"Se registró un nuevo abono para la venta con Folio: {venta.Folio} por un total de {montoAbonado} por el Usuario: {usuario.UserName}",
                        ReferenciaId = abono.AbonoId.ToString(),
                        TipoReferencia = "Add"
                    };

                    await _dbContext.Bitacora.AddAsync(bitacora);

                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();

                    var pdfBytes = _reciboPdfService.GenerarReciboAbonoPDF(venta, abono, cambio);

                    return new CreateAbonoResult
                    {
                        AbonoId = abono.AbonoId,
                        PdfBlob = pdfBytes
                    };

                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("Error al procesar el abono.", ex);
                }
            }
        }

        public async Task<bool> DeleteAbono(int abonoId, string userName)
        {
            try
            {
                var abono = await _dbContext.Abono
                    .Include(a => a.Venta)
                    .FirstOrDefaultAsync(a => a.AbonoId == abonoId);

                if (abono == null)
                    throw new Exception("Abono no encontrado");

                // Obtener la venta relacionada
                var venta = abono.Venta;

                // Eliminar el abono de la base de datos
                _dbContext.Abono.Remove(abono);
                await _dbContext.SaveChangesAsync();

                // Recalcular el saldo pendiente: total de la venta - suma de los abonos restantes
                var totalAbonos = await _dbContext.Abono
                    .Where(a => a.VentaId == venta.VentaId)
                    .SumAsync(a => a.Monto);

                venta.SaldoPendiente = venta.Total - totalAbonos;

                // Actualizar el estado de la venta según el nuevo saldo pendiente
                venta.EstadoVenta = venta.SaldoPendiente > 0 ? VentaEstado.Pendiente : VentaEstado.Pagado;

                _dbContext.Ventas.Update(venta);

                var usuario = await _userManager.FindByNameAsync(userName);
                if (usuario == null) throw new Exception("Usuario no encontrado");

                var bitacora = new Bitacora
                {
                    UsuarioId = usuario.Id,
                    Fecha = DateTime.UtcNow,
                    Descripcion = $"Se eliminó un abono de {abono.Monto} para la venta con Folio: {venta.Folio} por el Usuario: {usuario.UserName}",
                    ReferenciaId = abono.AbonoId.ToString(),
                    TipoReferencia = "Delete"
                };

                await _dbContext.Bitacora.AddAsync(bitacora);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Abono> GeneratePdf(int abonoId)
        {
            var abono = await _dbContext.Abono
                .Include(a => a.Venta)
                    .ThenInclude(v => v.Cliente)
                .Include(a => a.Venta)
                    .ThenInclude(v => v.DetalleVentas)
                        .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(a => a.AbonoId == abonoId);

            if (abono == null)
                throw new Exception("Abono no encontrado");

            return abono;
        }
    }
}
