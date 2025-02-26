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
        public int PaymentId { get; set; }
        public DateTime FechaPago { get; set; }
        public int MesPagado { get; set; }  // 1 = Enero, 2 = Febrero...
        public int AñoPagado { get; set; }
        public decimal MontoPagado { get; set; }
        public string MetodoPago { get; set; }  // Efectivo, Tarjeta, Transferencia

        public int? StudentId { get; set; }
    }
}
