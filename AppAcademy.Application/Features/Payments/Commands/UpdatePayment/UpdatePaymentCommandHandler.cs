using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Application.Exceptions;
using AppAcademy.Domain.ControlAcademia;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Payments.Commands.UpdatePayment
{
    public class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILogger<UpdatePaymentCommandHandler> _logger;

        public UpdatePaymentCommandHandler(IPaymentRepository paymentRepository, ILogger<UpdatePaymentCommandHandler> logger)
        {
            _paymentRepository = paymentRepository;
            _logger = logger;
        }

        public async Task Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
        {
            var findPayment = await _paymentRepository.GetByIdInt(request.PaymentId);
            if (findPayment == null)
            {
                _logger.LogError($"No se encontro el id del pago {request.PaymentId}");
                throw new NotFoundException(nameof(Payment), request.PaymentId);
            }

            findPayment.FechaPago = request.FechaPago;
            findPayment.MesPagado = request.MesPagado;
            findPayment.AñoPagado = request.AñoPagado;
            findPayment.MontoPagado = request.MontoPagado;
            findPayment.StudentId = request.StudentId;

            await _paymentRepository.UpdateAsync(findPayment);
            _logger.LogInformation($"La operacion fue exitosa {request.PaymentId}");
        }
    }
}
