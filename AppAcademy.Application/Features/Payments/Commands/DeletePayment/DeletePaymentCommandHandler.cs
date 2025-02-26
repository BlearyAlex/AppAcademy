using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Payments.Commands.DeletePayment
{
    public class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILogger<DeletePaymentCommandHandler> _logger;

        public DeletePaymentCommandHandler(IPaymentRepository paymentRepository, ILogger<DeletePaymentCommandHandler> logger)
        {
            _paymentRepository = paymentRepository;
            _logger = logger;
        }

        public async Task Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {
            var findPayment = await _paymentRepository.GetByIdInt(request.PaymentId);
            if (findPayment == null)
            {
                _logger.LogError($"{request.PaymentId} pago no existe en el sistema");
                throw new NotFoundException(nameof(findPayment), request.PaymentId);
            }

            await _paymentRepository.DeleteAsync(findPayment);

            return;
        }
    }
}
