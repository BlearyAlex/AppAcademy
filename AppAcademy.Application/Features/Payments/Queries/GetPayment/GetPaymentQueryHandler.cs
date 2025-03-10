using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Payments.Queries.GetPayment
{
    public class GetPaymentQueryHandler : IRequestHandler<GetPaymentQuery, GetPaymentVm>
    {
        private readonly IPaymentRepository _paymentRepository;

        public GetPaymentQueryHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<GetPaymentVm> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetPayment(request._PaymentId);
            return payment;
        }
    }
}
