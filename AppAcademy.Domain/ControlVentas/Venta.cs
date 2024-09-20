using AppAcademy.Domain.Auth;

namespace AppAcademy.Domain.PuntoDeVenta
{
    public class Venta
    {
        public string ventaId { get; set; } = Guid.NewGuid().ToString();
        public DateTime FechaCompra { get; set; }

        // Relaciones
        public string? ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        public string? UserId { get; set; }
        public User? User { get; set; }

        public List<DetalleCorte> DetalleCortes { get; set; } = [];
        public List<Devolucion> Devoluciones { get; set; } = [];
        public List<DetallePago> DetallePagos { get; set; } = [];
        public List<DetalleVenta> DetalleVentas { get; set; } = [];
    }
}
