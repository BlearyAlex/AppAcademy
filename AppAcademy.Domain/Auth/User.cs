namespace AppAcademy.Domain.Auth
{
    public class User
    {
        public string UserId { get; set; } = Guid.NewGuid().ToString();
        public string? NombreCompleto { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaUltimoAcceso { get; set; }

        // Relaciones
        public EstadoUser? EstadoUser { get; set; }
        public Rol? Rol { get; set; }

        public List<HistorialInventario> HistorialInventarios { get; set; } = [];
        public List<Salida> Salidas { get; set; } = [];
        public List<HistorialAcceso> HistorialAccesos { get; set; } = [];
        public List<Entrada> Entradas { get; set; } = [];
        public List<Venta> Ventas { get; set; } = [];
        public List<Devolucion> Devoluciones { get; set; } = [];
    }
}
