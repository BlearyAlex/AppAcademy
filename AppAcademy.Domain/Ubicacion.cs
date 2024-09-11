namespace AppAcademy.Domain
{
    public class Ubicacion
    {
        public string UbicacionId { get; set; } = Guid.NewGuid().ToString();
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }

        // Relaciones
        public List<Entrada> Entradas { get; set; } = [];
    }
}
