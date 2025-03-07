using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Application.Features.Payments.Commands.CreatePayment;
using AppAcademy.Domain.ControlAcademia;
using AppAcademy.Domain.Enum;
using AppAcademy.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AppAcademy.Infrastucture.Repositories.ControlAcademia
{
    public class PaymentRepository : AsyncRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Payment> CreatePayment(Payment payment)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    // Obtener el costo mensual de la carrera
                    var career = await _dbContext.Careers.FindAsync(payment.CareerId);
                    if (career == null)
                    {
                        throw new Exception("La carrera seleccionada no existe.");
                    }

                    // Verificar si el estudiante ya tiene un pago para ese mes/año
                    var existingPayment = await _dbContext.Payments
                        .FirstOrDefaultAsync(p => p.StudentId == payment.StudentId &&
                                                  p.MesPagado == payment.MesPagado &&
                                                  p.AnioPagado == payment.AnioPagado);

                    if (existingPayment != null)
                    {
                        throw new Exception("Ya existe un pago registrado para este mes y año.");
                    }

                    // Calcular total con descuento
                    decimal descuentoCalculado = career.CostoMensual * (payment.Descuento / 100);
                    decimal totalFinal = career.CostoMensual - descuentoCalculado;

                    // Asignar valores
                    payment.Total = totalFinal;
                    payment.SaldoPendiente = totalFinal; // Inicialmente, el saldo pendiente es el total
                    payment.EstadoVenta = VentaEstado.Pendiente;
                    payment.FechaPago = DateTime.Now;

                    // Guardar el pago
                    await _dbContext.Payments.AddAsync(payment);
                    await _dbContext.SaveChangesAsync();

                    await transaction.CommitAsync();
                    return payment;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("Error al registrar el pago: " + ex.Message, ex);
                }
            }
        }

        public async Task<bool> UpdatePaymentSaldo(Payment payment)
        {
            try
            {
                var existingPayment = await _dbContext.Payments
                    .Include(p => p.Career)
                    .FirstOrDefaultAsync(p => p.PaymentId == payment.PaymentId);

                if (existingPayment == null)
                    return false;

                existingPayment.SaldoPendiente = payment.SaldoPendiente;
                existingPayment.EstadoVenta = payment.EstadoVenta;
                existingPayment.FechaPago = payment.FechaPago;

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar pago.", ex);
            }
        }
    }
}
