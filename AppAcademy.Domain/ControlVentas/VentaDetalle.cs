

namespace AppAcademy.Domain.PuntoDeVenta
{
    public class VentaDetalle
    {
        public string VentaDetalleId { get; set; } = Guid.NewGuid().ToString();

        public string? VentaId { get; set; }
        public Venta? Venta { get; set; }

        public string? ProductoId { get; set; }
        public Producto? Producto { get; set; }

        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }

    }
}
