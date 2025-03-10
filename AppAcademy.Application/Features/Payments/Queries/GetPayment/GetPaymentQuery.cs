using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Payments.Queries.GetPayment
{
    public class GetPaymentQuery : IRequest<GetPaymentVm>
    {
        public int _PaymentId { get; set; }

        public GetPaymentQuery(int paymentId)
        {
            _PaymentId = paymentId;
        }
    }
}
