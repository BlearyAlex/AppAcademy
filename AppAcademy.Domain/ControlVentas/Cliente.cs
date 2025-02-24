namespace AppAcademy.Domain.PuntoDeVenta
{
    public class Cliente
    {
        public string ClienteId { get; set; } = Guid.NewGuid().ToString();
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public DateTime FechaRegistro { get; set; }

        // Relaciones
        public List<Venta> Ventas { get; set; } = [];
    }
}
