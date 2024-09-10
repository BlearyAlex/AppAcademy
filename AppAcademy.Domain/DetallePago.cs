namespace AppAcademy.Domain
{
    public class DetallePago
    {
        public string DetallePagoId { get; set; } = Guid.NewGuid().ToString();
        public TipoPago Tipo { get; set; }
        public decimal Monto { get; set; }

        // Enum para los tipos de pago
        public enum TipoPago
        {
            Efectivo,
            TarjetaDeCredito,
            Transferencia,
            Otro
        }

        // Relaciones
        public Venta? Venta { get; set; }
    }
}
