using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Application.Features.AbonosAcademy.Commands.CreateAbonoAcademy;
using AppAcademy.Domain.ControlAcademia;
using AppAcademy.Domain.Enum;
using AppAcademy.Domain.Logs;
using AppAcademy.Infrastucture.Identity;
using AppAcademy.Infrastucture.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AppAcademy.Infrastucture.Repositories.ControlAcademia
{
    public class AbonoAcademyRepository : AsyncRepository<AbonoAcademy>, IAbonoAcademyRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IReciboAbonoPdfService _reciboPdfService;

        public AbonoAcademyRepository(AppAcademyDbContext dbContext, UserManager<AppUser> userManager, IReciboAbonoPdfService reciboPdfService) : base(dbContext)
        {
            _userManager = userManager;
            _reciboPdfService = reciboPdfService;
        }

        public async Task<CreateAbonoAcademyResult> CreateAbonoAcademy(int paymentId, decimal montoAbonado, string userName)
        {
            if (montoAbonado <= 0) throw new ArgumentException("El monto abonado debe ser mayor que cero.");
            try
            {
                var payment = await _dbContext.Payments
                    .Include(p => p.Student)
                    .Include(p => p.Career)
                        .ThenInclude(a => a.AcademicCycles)
                    .Include(p => p.AbonosAcademy)
                    .FirstOrDefaultAsync(p => p.PaymentId == paymentId);
                    
                if (payment == null) throw new Exception("Pago no encontrada.");

                decimal cambio = 0;
                if (montoAbonado > payment.SaldoPendiente)
                {
                    cambio = montoAbonado - payment.SaldoPendiente;
                }

                var student = await _dbContext.Students.FindAsync(payment.StudentId);
                if (student == null) throw new Exception("Estudiante no encontrado");

                var abono = new AbonoAcademy
                {
                    PaymentId = paymentId,
                    Monto = montoAbonado,
                    FechaAbono = DateTime.UtcNow,
                    StudentId = payment.StudentId,
                };

                await _dbContext.AddAsync(abono);
                await _dbContext.SaveChangesAsync();

                // Ajustar el saldo pendiente
                if (montoAbonado >= payment.SaldoPendiente)
                {
                    payment.SaldoPendiente = 0;
                    payment.EstadoVenta = VentaEstado.Pagado;
                }
                else
                {
                    payment.SaldoPendiente -= montoAbonado;
                }

                var usuario = await _userManager.FindByNameAsync(userName);
                if (usuario == null) throw new Exception("Usuario no encontrado");

                var bitacora = new Bitacora
                {
                    UsuarioId = usuario.Id,
                    Fecha = DateTime.UtcNow,
                    Descripcion = $"Se registró un nuevo abono para el estudiante {student.Nombre} por un total de: {abono.Monto} correspondiente al mes: {payment.MesPagado} por el Usuario: {usuario.UserName}.",
                    ReferenciaId = abono.AbonoAcademyId.ToString(),
                    TipoReferencia = "Add"
                };

                _dbContext.Bitacora.Add(bitacora);

                await _dbContext.SaveChangesAsync();

                var pdfBytes = _reciboPdfService.GenerarReciboAbonoAcademyPDF(payment, abono, cambio);

                return new CreateAbonoAcademyResult
                {
                    AbonoAcademyId = abono.AbonoAcademyId,
                    PdfBlob = pdfBytes,
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar el abono.", ex);
            }
        }

        public async Task<bool> DeleteAbono(int abonoId, string userName)
        {
            try
            {
                var abono = await _dbContext.AbonoAcademy
                    .Include(a => a.Payment)
                    .FirstOrDefaultAsync(a => a.AbonoAcademyId == abonoId);

                if (abono == null)
                    throw new Exception("Abono no encontrado");

                // Obtener el student relacionado
                var student = await _dbContext.Students.FindAsync(abono.StudentId);
                if (student == null) throw new Exception("Estudiante no encontrado");

                // Obtener la venta relacionada
                var payment = abono.Payment;

                // Eliminar el abono de la base de datos
                _dbContext.AbonoAcademy.Remove(abono);
                await _dbContext.SaveChangesAsync();

                // Recalcular el saldo pendiente
                var totalAbonos = await _dbContext.AbonoAcademy
                    .Where(a => a.PaymentId == payment.PaymentId)
                    .SumAsync(a => a.Monto);

                payment.SaldoPendiente = payment.Total - totalAbonos;

                // Actualizar el estado de la venta segun el nuevo saldo pendiente
                payment.EstadoVenta = payment.SaldoPendiente > 0 ? VentaEstado.Pendiente : VentaEstado.Pagado;

                _dbContext.Payments.Update(payment);

                // Bitacora
                var usuario = await _userManager.FindByNameAsync(userName);
                if (usuario == null) throw new Exception("Usuario no encontrado");

                var bitacora = new Bitacora
                {
                    UsuarioId = usuario.Id,
                    Fecha = DateTime.UtcNow,
                    Descripcion = $"Se elimino un abono del estudiante {student.Nombre} por un total de: {abono.Monto} por el Usuario: {usuario.UserName}",
                    ReferenciaId = abono.AbonoAcademyId.ToString(),
                    TipoReferencia = "Delete"
                };

                _dbContext.Bitacora.Add(bitacora);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el abono.", ex);  // Mejorar el mensaje de error
            }
            }
        }
    }

