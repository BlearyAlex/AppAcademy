using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Payments.Commands.DeletePayment
{
    public class DeletePaymentCommand : IRequest
    {
        public int PaymentId { get; set; }
    }
}
