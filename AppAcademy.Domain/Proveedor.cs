namespace AppAcademy.Domain
{
    public class Proveedor
    {
        public string ProveedorId { get; set; } = Guid.NewGuid().ToString();
        public string? Nombre { get; set; }

        // Relaciones
        public List<Producto> Productos { get; set; } = [];
    }
}
