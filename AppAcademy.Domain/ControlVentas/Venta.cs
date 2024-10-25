using AppAcademy.Domain.Auth;
using AppAcademy.Domain.Enum;

namespace AppAcademy.Domain.PuntoDeVenta
{
    public class Venta
    {
        public string VentaId { get; set; } = Guid.NewGuid().ToString();
        public DateTime? FechaCompra { get; set; }
        public VentaEstado EstadoVenta { get; set; }
        public decimal Bruto { get; set; }

        // Relaciones
        public string? ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        public string? UserId { get; set; }
        public User? User { get; set; }

        public List<DetalleVenta> DetalleVentas { get; set; } = new List<DetalleVenta>();
    }
}
