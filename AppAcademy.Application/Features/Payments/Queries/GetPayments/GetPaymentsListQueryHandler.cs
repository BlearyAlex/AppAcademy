using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Payments.Queries.GetPayments
{
    public class GetPaymentsListQueryHandler : IRequestHandler<GetPaymentsListQuery, List<GetPaymentsVm>>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILogger<GetPaymentsListQueryHandler> _logger;

        public GetPaymentsListQueryHandler(IPaymentRepository paymentRepository, ILogger<GetPaymentsListQueryHandler> logger)
        {
            _paymentRepository = paymentRepository;
            _logger = logger;
        }

        public async Task<List<GetPaymentsVm>> Handle(GetPaymentsListQuery request, CancellationToken cancellationToken)
        {
            var payemts = await _paymentRepository.GetPayments();

            return payemts;
        }
    }
}
