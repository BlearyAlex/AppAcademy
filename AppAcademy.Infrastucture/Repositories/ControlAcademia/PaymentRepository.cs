using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Application.Features.Payments.Commands.CreatePayment;
using AppAcademy.Application.Features.Payments.Queries.GetPayment;
using AppAcademy.Application.Features.Payments.Queries.GetPayments;
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

        public async Task<List<GetPaymentsVm>> GetPayments()
        {
            try
            {
                var payments = await _dbContext.Payments
                    .Include(p => p.Career)
                    .ThenInclude(p => p.AcademicCycles)
                    .Include(p => p.Student)
                    .Include(p => p.AbonosAcademy)
                    .Select(p => new GetPaymentsVm
                    {
                        PaymentId = p.PaymentId,
                        FechaPago = p.FechaPago,
                        MesPagado = p.MesPagado.ToString(),
                        AnioPagado = p.AnioPagado,
                        Total = p.Total,
                        SaldoPendiente = p.SaldoPendiente,
                        EstadoVenta = p.EstadoVenta.ToString(),
                        Career = p.Career != null
                            ? new GetPaymentWithCareer
                            {
                                CareerId = p.Career.CareerId,
                                Nombre = p.Career.Nombre,
                                Color = p.Career.Color
                            }
                            : null,
                        Student = p.Student != null
                            ? new GetPaymentWithStudent
                            {
                                StudentId = p.Student.StudentId,
                                Nombre = p.Student.Nombre,
                                Apellido = p.Student.Apellido,
                                AcademicCyleId = p.Student.AcademicCycleId ?? 0
                            }
                            : null,
                        Abonos = p.AbonosAcademy != null
                        ? p.AbonosAcademy.Select(a => new GetPaymentWithAbono
                        {
                            AbonoAcademyId = a.AbonoAcademyId,
                            Monto = a.Monto,
                            Fecha = a.FechaAbono
                        }).ToList()
                        : new List<GetPaymentWithAbono>()
                    }).ToListAsync();

                return payments;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al traer los pagos.", ex);
            }
        }

        public async Task<GetPaymentVm> GetPayment(int paymentId)
        {
            try
            {
                var payment = await _dbContext.Payments
                    .Include(p => p.Career)
                    .Include(p => p.Student)
                    .Include(p => p.AbonosAcademy)
                    .Where(p => p.PaymentId == paymentId)
                    .Select(p => new GetPaymentVm
                    {
                        PaymentId = p.PaymentId,
                        FechaPago = p.FechaPago,
                        MesPagado = p.MesPagado.ToString(),
                        AnioPagado = p.AnioPagado,
                        Total = p.Total,
                        SaldoPendiente = p.SaldoPendiente,
                        EstadoVenta = p.EstadoVenta.ToString(),
                        Career = p.Career != null
                            ? new GetPaymentWithCareer
                            {
                                CareerId = p.Career.CareerId,
                                Nombre = p.Career.Nombre,
                                Color = p.Career.Color
                            }
                            : null,
                        Student = p.Student != null
                            ? new GetPaymentWithStudent
                            {
                                StudentId = p.Student.StudentId,
                                Nombre = p.Student.Nombre,
                                Apellido = p.Student.Apellido
                            }
                            : null,
                        Abonos = p.AbonosAcademy != null
                        ? p.AbonosAcademy.Select(a => new GetPaymentWithAbono
                        {
                            AbonoAcademyId = a.AbonoAcademyId,
                            Monto = a.Monto,
                            Fecha = a.FechaAbono
                        }).ToList()
                        : new List<GetPaymentWithAbono>()
                    }).FirstOrDefaultAsync();

                return payment;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al traer el pago.", ex);
            }
        }
    }
}
