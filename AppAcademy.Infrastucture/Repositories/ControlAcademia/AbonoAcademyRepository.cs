using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Domain.ControlAcademia;
using AppAcademy.Domain.Enum;
using AppAcademy.Infrastucture.Historicos;
using AppAcademy.Infrastucture.Identity;
using AppAcademy.Infrastucture.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AppAcademy.Infrastucture.Repositories.ControlAcademia
{
    public class AbonoAcademyRepository : AsyncRepository<AbonoAcademy>, IAbonoAcademyRepository
    {
        private readonly UserManager<AppUser> _userManager;

        public AbonoAcademyRepository(AppAcademyDbContext dbContext, UserManager<AppUser> userManager) : base(dbContext)
        {
            _userManager = userManager;
        }

        public async Task<bool> CreateAbonoAcademy(int paymentId, decimal montoAbonado, string userName)
        {
            if (montoAbonado <= 0) return false;

            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var payment = await _dbContext.Payments.FindAsync(paymentId);
                    if (payment == null) return false;

                    var student = await _dbContext.Students.FindAsync(payment.StudentId);
                    if (student == null) throw new Exception("Estudiante no encontrado");

                    var abono = new AbonoAcademy
                    {
                        PaymentId = paymentId,
                        Monto = montoAbonado,
                        FechaAbono = DateTime.UtcNow
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
                        Descripcion = $"Se registró un nuevo abono del estudiante {student.Nombre} por un total de: {abono.Monto}",
                        ReferenciaId = abono.AbonoAcademyId.ToString(),
                        TipoReferencia = "Payment"
                    };

                    _dbContext.Bitacora.Add(bitacora);

                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync(); // Confirmar transacción

                    return true; 
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(); // Revertir si hay error
                    throw new Exception("Error al procesar el abono.", ex);
                }
            }
        }

        public async Task<bool> DeleteAbono(int abonoId, string userName)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
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
                        Descripcion = $"Se elimino un abono del estudiante {student.Nombre} por un total de: {abono.Monto}",
                        ReferenciaId = abono.AbonoAcademyId.ToString(),
                        TipoReferencia = "Payment"
                    };

                    _dbContext.Bitacora.Add(bitacora);
                    await _dbContext.SaveChangesAsync();

                    await transaction.CommitAsync(); // Confirmar transacción

                    return true;
                }
                catch (Exception ex)
                {
                    // Registrar el error si es necesario
                    await transaction.RollbackAsync(); // Revertir si hay error
                    throw new Exception("Error al eliminar el abono.", ex);  // Mejorar el mensaje de error
                }
            }
        }
    }
}
