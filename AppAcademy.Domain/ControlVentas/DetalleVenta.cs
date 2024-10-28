using AppAcademy.Domain.Enum;

namespace AppAcademy.Domain.PuntoDeVenta
{
    public class DetalleVenta
    {
        public string DetalleVentaId { get; set; } = Guid.NewGuid().ToString();
        public decimal Costo { get; set; }
        public int Cantidad { get; set; }


        // Relaciones
        public string? VentaId { get; set; }
        public Venta? Venta { get; set; }

        public string? ProductoId { get; set; }
        public Producto? Producto { get; set; }

    }
}
