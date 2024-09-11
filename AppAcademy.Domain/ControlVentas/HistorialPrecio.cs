namespace AppAcademy.Domain.PuntoDeVenta
{
    public class HistorialPrecio
    {
        public string HistorialPrecioId { get; set; } = Guid.NewGuid().ToString();
        public decimal Precio { get; set; }
        public decimal PrecioAnterior { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        // Relaciones
        public Producto? Producto { get; set; }
    }
}
