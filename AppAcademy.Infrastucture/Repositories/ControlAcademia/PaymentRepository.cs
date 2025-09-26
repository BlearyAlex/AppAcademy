using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Application.Exceptions;
using AppAcademy.Application.Features.Payments.Commands.DeletePayment;
using AppAcademy.Application.Features.Payments.Queries.GetPayment;
using AppAcademy.Application.Features.Payments.Queries.GetPayments;
using AppAcademy.Domain.ControlAcademia;
using AppAcademy.Domain.Enum;
using AppAcademy.Domain.Logs;
using AppAcademy.Infrastucture.Identity;
using AppAcademy.Infrastucture.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Infrastucture.Repositories.ControlAcademia
{
    public class PaymentRepository : AsyncRepository<Payment>, IPaymentRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<PaymentRepository> _logger;
        private readonly IAbonoAcademyRepository _abonoAcademyRepository;

        public PaymentRepository(AppAcademyDbContext dbContext, UserManager<AppUser> userManager, ILogger<PaymentRepository> logger, IAbonoAcademyRepository abonoAcademyRepository) : base(dbContext)
        {
            _userManager = userManager;
            _logger = logger;
            _abonoAcademyRepository = abonoAcademyRepository;
        }

        public async Task<Payment> CreatePayment(Payment payment, string userName)
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

                // Obtener datos del estudiante
                var student = await _dbContext.Students.FindAsync(payment.StudentId);
                if (student == null) throw new Exception("Estudiante no encontrado");

                // Calcular total con descuento
                decimal descuentoCalculado = career.CostoMensual * (payment.Descuento / 100);
                decimal totalFinal = Math.Max(career.CostoMensual - descuentoCalculado, 0);

                // Asignar valores
                payment.Total = totalFinal;
                payment.SaldoPendiente = totalFinal; // Inicialmente, el saldo pendiente es el total
                payment.EstadoVenta = VentaEstado.Pendiente;
                payment.FechaPago = DateTime.Now;

                // Guardar el pago
                await _dbContext.Payments.AddAsync(payment);

                var usuario = await _userManager.FindByNameAsync(userName);
                if (usuario == null) throw new Exception("Usuario no encontrado");

                var bitacora = new Bitacora
                {
                    UsuarioId = usuario.Id,
                    Fecha = DateTime.UtcNow,
                    Descripcion = $"Se registró un nuevo pago para el estudiante {student.Nombre + " " + student.Apellido} por un total de: {payment.Total} por el Usuario: {usuario.UserName}",
                    ReferenciaId = payment.PaymentId.ToString(),
                    TipoReferencia = "Add"
                };

                await _dbContext.Bitacora.AddAsync(bitacora);
                await _dbContext.SaveChangesAsync();

                return payment;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al registrar el pago para el estudiante {StudentId}", payment.StudentId);
                throw;
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

        public async Task<bool> DeletePayment(DeletePaymentCommand payment, string userName)
        {
            var findPayment = await _dbContext.Payments
                .Include(p => p.AbonosAcademy)
                .FirstOrDefaultAsync(p => p.PaymentId == payment.PaymentId);

            if (findPayment == null)
            {
                _logger.LogError($"{payment.PaymentId} pago no existe en el sistema");
                throw new NotFoundException(nameof(findPayment), payment.PaymentId);
            }

            foreach (var abono in findPayment.AbonosAcademy.ToList())
            {
                _dbContext.AbonoAcademy.Remove(abono);
            }

            // Obtener datos del estudiante
            var student = await _dbContext.Students.FindAsync(findPayment.StudentId);
            if (student == null) throw new Exception("Estudiante no encontrado");

            _dbContext.Remove(findPayment);

            var usuario = await _userManager.FindByNameAsync(userName);
            if (usuario == null) throw new Exception("Usuario no encontrado");

            // Mensaje dinámico para bitácora
            string mensajeBitacora = $"Se eliminó el pago del mes: {findPayment.MesPagado} del estudiante {student.Nombre} por el Total: {findPayment.Total} {student.Apellido} por el Usuario: {usuario.UserName}.";

            if (findPayment.AbonosAcademy.Any())
            {
                decimal totalAbonado = findPayment.AbonosAcademy.Sum(a => a.Monto);
                mensajeBitacora += $" El pago tenía {findPayment.AbonosAcademy.Count} abonos por un total de ${totalAbonado}.";
            }

            var bitacora = new Bitacora
            {
                UsuarioId = usuario.Id,
                Fecha = DateTime.UtcNow,
                Descripcion = mensajeBitacora,
                ReferenciaId = findPayment.PaymentId.ToString(),
                TipoReferencia = "Delete"
            };

            _dbContext.Bitacora.Add(bitacora);

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
