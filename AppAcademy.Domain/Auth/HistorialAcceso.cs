namespace AppAcademy.Domain.Auth
{
    public class HistorialAcceso
    {
        public string HistorialAccesoId { get; set; } = Guid.NewGuid().ToString();
        public DateTime FechaAcceso { get; set; }
        public string? DireccionIp { get; set; }
        public string? Dispositivo { get; set; }
        public string? TipoAcceso { get; set;}

        // Relaciones
        public User? User { get; set; }
    }
}
