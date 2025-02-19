using AppAcademy.Domain.PuntoDeVenta;

namespace AppAcademy.Domain.ControlVentas
{
    public class Abono
    {
        public int AbonoId { get; set; }

        public string VentaId { get; set; }
        public Venta Venta { get; set; }

        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
    }
}
