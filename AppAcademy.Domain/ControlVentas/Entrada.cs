namespace AppAcademy.Domain.PuntoDeVenta
{
    public class Entrada
    {
        public string EntradaId { get; set; } = Guid.NewGuid().ToString();
        public int TotalProductosEntrada { get; set; }
        public DateTime FechaDeEmision { get; set; }
        public string? NumeroFactura { get; set; }
        public string? Folio { get; set; }
        public decimal Bruto { get; set; }

        // Relaciones
        public List<EntradaProducto> EntradaProductos { get; set; } = new List<EntradaProducto>();
    }
}
