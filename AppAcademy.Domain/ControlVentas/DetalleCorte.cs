using AppAcademy.Domain.Enum;

namespace AppAcademy.Domain.PuntoDeVenta
{
    public class DetalleCorte
    {
        public string DetalleCorteId { get; set; } = Guid.NewGuid().ToString();
        public TipoPagoEstado EstadoTipoPago { get; set; }
        public decimal Monto { get; set; }
        public CorteEstado EstadoCorte { get; set; }

        // Relaciones
        public string? CorteId { get; set; }
        public Corte? Corte { get; set; }
    }
}
