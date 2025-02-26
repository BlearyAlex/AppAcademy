using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Application.Exceptions;
using AppAcademy.Domain.ControlAcademia;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.StudentPaymentStatuses.Commands.UpdateStudentPaymentStatus
{
    public class UpdateStudentPaymentStatusCommandHandler : IRequestHandler<UpdateStudentPaymentStatusCommand>
    {
        private readonly IStudentPaymentStatusRepository _studentPaymentStatusRepository;
        private readonly ILogger<UpdateStudentPaymentStatusCommandHandler> _logger;

        public UpdateStudentPaymentStatusCommandHandler(IStudentPaymentStatusRepository studentPaymentStatusRepository, ILogger<UpdateStudentPaymentStatusCommandHandler> logger)
        {
            _studentPaymentStatusRepository = studentPaymentStatusRepository;
            _logger = logger;
        }

        public async Task Handle(UpdateStudentPaymentStatusCommand request, CancellationToken cancellationToken)
        {
            var findStudentPayment = await _studentPaymentStatusRepository.GetByIdInt(request.StudentPaymentStatusId);

            if (findStudentPayment == null)
            {
                _logger.LogError($"No se encontro el id del career {request.StudentPaymentStatusId}");
                throw new NotFoundException(nameof(StudentPaymentStatus), request.StudentPaymentStatusId);
            }

            findStudentPayment.Mes = request.Mes;
            findStudentPayment.Año = request.Año;
            findStudentPayment.Pagado = request.Pagado;
            findStudentPayment.StudentId = request.StudentId;

            await _studentPaymentStatusRepository.UpdateAsync(findStudentPayment);

            _logger.LogInformation($"La operacion fue exitosa {request.StudentPaymentStatusId}");
        }
    }
}
