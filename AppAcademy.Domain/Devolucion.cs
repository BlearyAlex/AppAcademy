using AppAcademy.Domain.Auth;

namespace AppAcademy.Domain
{
    public class Devolucion
    {
        public string DevolucionId { get; set; } = Guid.NewGuid().ToString();
        public decimal Cantidad { get; set; }
        public string? Motivo { get; set; }
        public DateTime FechaDevolucion { get; set; }

        // Relaciones
        public Venta? Venta { get; set; }
        public Producto? Producto { get; set; }
        public User? User { get; set; }
    }
}
