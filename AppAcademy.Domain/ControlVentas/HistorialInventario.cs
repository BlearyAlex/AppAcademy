using AppAcademy.Domain.Auth;

namespace AppAcademy.Domain.PuntoDeVenta
{
    public class HistorialInventario
    {
        public string HistorialInventarioId { get; set; } = Guid.NewGuid().ToString();
        public int CantidadAnterior { get; set; }
        public int CantidadNueva { get; set; }
        public DateTime FechaCambio { get; set; }
        public string? Motivo { get; set; }

        // Relaciones
        public User? User { get; set; }
        public Producto? Producto { get; set; }
    }
}
