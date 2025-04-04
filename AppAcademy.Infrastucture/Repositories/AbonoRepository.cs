using AppAcademy.Application.Contracts.Persistence;
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

        public AbonoRepository(AppAcademyDbContext dbContext, UserManager<AppUser> userManager, IBitacoraRepository bitacoraRepository) : base(dbContext)
        {
            _userManager = userManager;
            _bitacoraRepository = bitacoraRepository;
        }

        public async Task<bool> CreateAbono(string ventaId, decimal montoAbonado, string userName)
        {
            if (montoAbonado <= 0) return false;

            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var venta = await _dbContext.Ventas.FindAsync(ventaId);
                    if (venta == null) return false;

                    var abono = new Abono
                    {
                        VentaId = ventaId,
                        Monto = montoAbonado,
                        Fecha = DateTime.Now
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
                        Descripcion = $"Se registró un nuevo abono para la venta con Folio: {venta.Folio} por un total de {montoAbonado} por el usuario {usuario.UserName}",
                        ReferenciaId = abono.AbonoId.ToString(),
                        TipoReferencia = "Abono"
                    };

                    await _dbContext.Bitacora.AddAsync(bitacora);

                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return true;

                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
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
                    Descripcion = $"Se eliminó un abono de {abono.Monto} para la venta con Folio: {venta.Folio} por el usuario {usuario.UserName}",
                    ReferenciaId = abono.AbonoId.ToString(),
                    TipoReferencia = "Abono"
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
    }
}
