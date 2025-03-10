using AppAcademy.Application.Features.Payments.Queries.GetPayments;
using AppAcademy.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Payments.Queries.GetPayment
{
    public class GetPaymentVm
    {
        public int PaymentId { get; set; }
        public DateTime FechaPago { get; set; }
        public string MesPagado { get; set; }
        public int AnioPagado { get; set; }
        public decimal Total { get; set; }
        public decimal SaldoPendiente { get; set; }
        public string EstadoVenta { get; set; }
        public GetPaymentWithCareer Career { get; set; }
        public GetPaymentWithStudent Student { get; set; }
        public List<GetPaymentWithAbono> Abonos { get; set; }
    }
}
