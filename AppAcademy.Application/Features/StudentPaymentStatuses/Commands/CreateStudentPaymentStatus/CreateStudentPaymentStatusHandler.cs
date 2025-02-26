using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Domain.ControlAcademia;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.StudentPaymentStatuses.Commands.CreateStudentPaymentStatus
{
    public class CreateStudentPaymentStatusHandler : IRequestHandler<CreateStudentPaymentStatusComand, int>
    {
        private readonly IStudentPaymentStatusRepository _studentPaymentStatusRepository;
        private readonly ILogger<CreateStudentPaymentStatusHandler> _logger;

        public CreateStudentPaymentStatusHandler(IStudentPaymentStatusRepository studentPaymentStatusRepository, ILogger<CreateStudentPaymentStatusHandler> logger)
        {
            _studentPaymentStatusRepository = studentPaymentStatusRepository;
            _logger = logger;
        }

        public async Task<int> Handle(CreateStudentPaymentStatusComand request, CancellationToken cancellationToken)
        {
            var studentPayment = new StudentPaymentStatus
            {
                Mes = request.Mes,
                Año = request.Año,
                Pagado = request.Pagado,
                StudentId = request.StudentId,
            };

            var newStudentPayment = await _studentPaymentStatusRepository.AddAsync(studentPayment);

            return newStudentPayment.StudentPaymentStatusId;
        }
    }
}
