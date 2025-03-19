using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Payments.Commands.DeletePayment
{
    public class DeletePaymentCommand : IRequest<bool>
    {
        public int PaymentId { get; set; }

        public string UserName { get; set; } = string.Empty;
    }
}
