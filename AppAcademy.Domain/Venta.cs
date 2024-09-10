using AppAcademy.Domain.Auth;

namespace AppAcademy.Domain
{
    public class Venta
    {
        public string ventaId { get; set; } = Guid.NewGuid().ToString();
        public DateTime FechaCompra { get; set; }

        // Relaciones
        public Cliente? Cliente { get; set; }
        public User? User { get; set; }

        public List<DetalleCorte> DetalleCortes { get; set; } = [];
        public List<Devolucion> Devoluciones { get; set; } = [];
        public List<DetallePago> DetallePagos { get; set; } = [];
        public List<DetalleVenta> DetalleVentas { get; set; } = [];
    }
}
