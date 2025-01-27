using AppAcademy.Domain.Enum;

namespace AppAcademy.Domain.PuntoDeVenta
{
    public class Venta
    {
        public string VentaId { get; set; } = Guid.NewGuid().ToString();
        public DateTime FechaCompra { get; set; }
        public VentaEstado EstadoVenta { get; set; }
        public TipoPagoEstado EstadoTipoPago { get; set; }
        public decimal Bruto { get; set; }
        public decimal? Descuento { get; set; }
        public decimal Neto { get; set; }
        public int TotalProductos { get; set; }

        // Relaciones
        public string? ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        public List<DetalleVenta> DetalleVentas { get; set; } = new List<DetalleVenta>();
    }
}
