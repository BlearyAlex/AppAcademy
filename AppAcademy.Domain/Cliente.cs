namespace AppAcademy.Domain
{
    public class Cliente
    {
        public string ClienteId { get; set; } = Guid.NewGuid().ToString();
        public string? NombreCompleto { get; set; }
        public string? Email { get; set; }
        public int Telefono { get; set; }
        public string? Direccion { get; set; }
        public DateTime FechaRegistro { get; set; }

        // Relaciones
        public List<Venta> Ventas { get; set; } = [];
    }
}
