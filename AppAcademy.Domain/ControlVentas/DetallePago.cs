using AppAcademy.Domain.Enum;

namespace AppAcademy.Domain.PuntoDeVenta
{
    public class DetallePago
    {
        public string DetallePagoId { get; set; } = Guid.NewGuid().ToString();
        public TipoPago Tipo { get; set; } 
        public decimal Monto { get; set; }


        // Relaciones
        public Venta? Venta { get; set; }
    }
}
