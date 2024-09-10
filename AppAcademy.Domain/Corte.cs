using AppAcademy.Domain.Auth;

namespace AppAcademy.Domain
{
    public class Corte
    {
        public string CorteId { get; set; } = Guid.NewGuid().ToString();
        public DateTime FechaCorte { get; set; }
        public decimal TotalEfectivo { get; set; }
        public decimal TotalTarjeta { get; set; }
        public decimal TotalVales { get; set; }
        public decimal TotalDevoluciones { get; set; }
        public string? Comentarios { get; set; }

        // Relaciones
        public User? User { get; set; }

        public List<DetalleCorte> DetalleCortes { get; set; } = [];
    }
}
