namespace AppAcademy.Domain
{
    public class Promocion
    {
        public string PromocionId { get; set; } = Guid.NewGuid().ToString();
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int Descuento { get; set; }

        // Relaciones
        public List<Producto> Productos { get; set; } = [];
    }
}
