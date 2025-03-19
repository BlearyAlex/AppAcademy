using AppAcademy.Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Payments.Commands.CreatePayment
{
    public class CreatePaymentCommand : IRequest<int>
    {
        public int CareerId { get; set; }
        public int StudentId { get; set; }
        public MesEstado MesPagado { get; set; }
        public int AnioPagado { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }
        public decimal SaldoPendiente { get; set; }

        public string UserName { get; set; } = string.Empty;
    }
}
