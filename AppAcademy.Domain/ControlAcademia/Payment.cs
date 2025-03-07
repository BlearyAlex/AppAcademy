using AppAcademy.Domain.Enum;

namespace AppAcademy.Domain.ControlAcademia
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public DateTime FechaPago { get; set; }
        public MesEstado MesPagado { get; set; }  
        public int AnioPagado { get; set; }
        public decimal Total { get; set; }
        public decimal SaldoPendiente { get; set; }
        public decimal Descuento { get; set; }
        public VentaEstado EstadoVenta { get; set; }

        public int? StudentId { get; set; }
        public Student Student { get; set; }

        public int? CareerId { get; set; }
        public Career Career { get; set; }

        public virtual List<AbonoAcademy> AbonosAcademy { get; set; }
    }
}
