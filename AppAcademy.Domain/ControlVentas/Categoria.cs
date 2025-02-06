namespace AppAcademy.Domain.PuntoDeVenta
{
    public class Categoria
    {
        public string CategoriaId { get; set; } = Guid.NewGuid().ToString();
        public string? Nombre { get; set; }
        public string? Color { get; set; }

        // Relaciones
        public List<Producto> Productos { get; set; } = [];
    }
}
