using AppAcademy.Domain.Enum;

namespace AppAcademy.Domain.PuntoDeVenta
{
    public class DetalleVenta
    {
        public string DetalleVentaId { get; set; } = Guid.NewGuid().ToString();
        public TipoPago Tipo { get; set; }
        public decimal Monto { get; set; }
        public int DescuentoProducto { get; set; }


        // Relaciones
        public Venta? Venta { get; set; }
        public Producto? Producto { get; set; }

    }
}
