using AppAcademy.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Payments.Queries.GetPayments
{
    public class GetPaymentsVm
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

    public class GetPaymentWithStudent
    {
        public int StudentId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int AcademicCyleId { get; set; }
    }

    public class GetPaymentWithCareer
    {
        public int CareerId { get; set; }
        public string Nombre { get; set; }
        public string Color { get; set; }
    }

    public class GetPaymentWithAbono
    {
        public int AbonoAcademyId { get; set; }
        public int PaymenteId { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
    }
}
