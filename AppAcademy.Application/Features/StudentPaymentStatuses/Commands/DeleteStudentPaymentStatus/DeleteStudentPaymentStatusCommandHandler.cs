using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.StudentPaymentStatuses.Commands.DeleteStudentPaymentStatus
{
    public class DeleteStudentPaymentStatusCommandHandler : IRequestHandler<DeleteStudentPaymentStatusCommand>
    {
        private readonly IStudentPaymentStatusRepository _studentPaymentStatusRepository;
        private readonly ILogger<DeleteStudentPaymentStatusCommandHandler> _logger;

        public DeleteStudentPaymentStatusCommandHandler(IStudentPaymentStatusRepository studentPaymentStatusRepository, ILogger<DeleteStudentPaymentStatusCommandHandler> logger)
        {
            _studentPaymentStatusRepository = studentPaymentStatusRepository;
            _logger = logger;
        }

        public async Task Handle(DeleteStudentPaymentStatusCommand request, CancellationToken cancellationToken)
        {
            var findStudentPayment = await _studentPaymentStatusRepository.GetByIdInt(request.StudentPaymentStatusId);
            if (findStudentPayment == null)
            {
                _logger.LogError($"{request.StudentPaymentStatusId} career no existe en el sistema");
                throw new NotFoundException(nameof(findStudentPayment), request.StudentPaymentStatusId);
            }

            await _studentPaymentStatusRepository.DeleteAsync(findStudentPayment);

            return;
        }
    }
}
