using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.ControlVentas;
using AppAcademy.Domain.Enum;
using AppAcademy.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                // Ajustar el saldo pendiente de la venta al monto del abono eliminado
                venta.SaldoPendiente += abono.Monto;

                // Si el saldo pendiente es mayor a 0, cambiar el estado a 'Pendiente'
                if (venta.SaldoPendiente > 0)
                {
                    venta.EstadoVenta = VentaEstado.Pendiente;
                }
                // Si el saldo pendiente es 0, marcar la venta como 'Pagada'
                else
                {
                    venta.EstadoVenta = VentaEstado.Pagado;
                }

                // Eliminar el abono de la base de datos
                _dbContext.Abono.Remove(abono);

                // Actualizar la venta
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
