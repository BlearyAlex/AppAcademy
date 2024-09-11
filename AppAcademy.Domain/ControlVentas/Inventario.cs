namespace AppAcademy.Domain.PuntoDeVenta
{
    public class Inventario
    {
        public string InventarioId { get; set; } = Guid.NewGuid().ToString();
        public int Cantidad { get; set; }
        public DateTime FechaUltimaActualizacion { get; set; }
        public string? Ubicacion { get; set; }
        public string? Lote { get; set; }
        public DateTime FechaExpiracion { get; set; }

        // Relaciones
        public Producto? Producto { get; set; }
    }
}
