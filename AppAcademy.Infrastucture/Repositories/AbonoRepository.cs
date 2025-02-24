using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.ControlVentas;
using AppAcademy.Domain.Enum;
using AppAcademy.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AppAcademy.Infrastucture.Repositories
{
    public class AbonoRepository : AsyncRepository<Abono>, IAbonoRepository
    {
        public AbonoRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Abono> CreateAbono(Abono abono)
        {
            try
            {
                await _dbContext.Abono.AddAsync(abono);
                await _dbContext.SaveChangesAsync();
                return abono;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAbono(int abonoId)
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
