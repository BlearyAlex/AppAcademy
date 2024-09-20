namespace AppAcademy.Domain.PuntoDeVenta
{
    public class EntradaProducto
    {
        public string EntradaProductoId { get; set; } = Guid.NewGuid().ToString();
        public int Cantidad { get; set; }
        public decimal Costo { get; set; }

        // Relaciones
        public string? EntradaId { get; set; }
        public Entrada? Entrada { get; set; }

        public string? MyProperty { get; set; }
        public Producto? Producto { get; set; }
    }
}
