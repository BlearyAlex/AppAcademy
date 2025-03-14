using AppAcademy.Domain.ControlVentas;
using AppAcademy.Domain.Enum;

namespace AppAcademy.Domain.PuntoDeVenta
{
    public class Venta
    {
        public string VentaId { get; set; } = Guid.NewGuid().ToString();
        public DateTime Fecha { get; set; }

        public string? ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        public decimal Total { get; set; }
        public decimal SaldoPendiente { get; set; }
        public decimal Descuento { get; set; }
        public decimal Impuesto { get; set; }
        public string Folio { get; set; }
        public VentaEstado EstadoVenta { get; set; }
        public List<VentaDetalle> DetalleVentas { get; set; } = new List<VentaDetalle>();
        public List<Abono> Abonos { get; set; }

        public decimal TotalFinal => Total - Descuento + Impuesto;
    }
}
