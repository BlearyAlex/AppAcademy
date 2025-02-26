using MediatR;

namespace AppAcademy.Application.Features.StudentPaymentStatuses.Commands.UpdateStudentPaymentStatus
{
    public class UpdateStudentPaymentStatusCommand : IRequest
    {
        public int StudentPaymentStatusId { get; set; }
        public int Mes { get; set; }  // 1 = Enero, 2 = Febrero...
        public int Año { get; set; }
        public bool Pagado { get; set; }  // TRUE = Pagado, FALSE = Pendiente

        public int? StudentId { get; set; }
    }
}
