using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Domain.ControlAcademia;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, int>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILogger<CreatePaymentCommandHandler> _logger;

        public CreatePaymentCommandHandler(IPaymentRepository paymentRepository, ILogger<CreatePaymentCommandHandler> logger)
        {
            _paymentRepository = paymentRepository;
            _logger = logger;
        }

        public async Task<int> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = new Payment
            {
                FechaPago = DateTime.UtcNow,
                MesPagado = request.MesPagado,
                AnioPagado = request.AnioPagado,
                Descuento = request.Descuento,
                Total = request.Total,
                SaldoPendiente = request.SaldoPendiente,
                StudentId = request.StudentId,
                CareerId = request.CareerId,
            };

            var newPayment = await _paymentRepository.AddAsync(payment);

            return newPayment.PaymentId;
        }
    }
}
