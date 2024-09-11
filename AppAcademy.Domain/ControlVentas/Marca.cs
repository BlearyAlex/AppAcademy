namespace AppAcademy.Domain.PuntoDeVenta
{
    public class Marca
    {
        public string MarcaId { get; set; } = Guid.NewGuid().ToString();
        public string? Nombre { get; set; }

        // Relaciones
        public List<Producto> Productos { get; set; } = [];
    }
}
