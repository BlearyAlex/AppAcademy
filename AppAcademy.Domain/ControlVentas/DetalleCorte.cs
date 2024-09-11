namespace AppAcademy.Domain.PuntoDeVenta
{
    public class DetalleCorte
    {
        public string DetalleCorteId { get; set; } = Guid.NewGuid().ToString();
        public string? TipoPago { get; set; }
        public decimal Monto { get; set; }

        // Relaciones
        public Corte? Corte { get; set; }
        public Venta? Venta { get; set; }
    }
}
