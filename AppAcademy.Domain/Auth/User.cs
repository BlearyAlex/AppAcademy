using AppAcademy.Domain.ControlAcademia;
using AppAcademy.Domain.PuntoDeVenta;

namespace AppAcademy.Domain.Auth
{
    public class User
    {
        public string UserId { get; set; } = Guid.NewGuid().ToString();
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaUltimoAcceso { get; set; }

        // Relaciones
        public string? EstadoUserId { get; set; }
        public EstadoUser? EstadoUser { get; set; }

        public string? RolId { get; set; }
        public Rol? Rol { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

        public List<HistorialInventario> HistorialInventarios { get; set; } = [];
        public List<Salida> Salidas { get; set; } = [];
        public List<HistorialAcceso> HistorialAccesos { get; set; } = [];
        public List<Entrada> Entradas { get; set; } = [];
        public List<Venta> Ventas { get; set; } = [];
        public List<Devolucion> Devoluciones { get; set; } = [];

        public List<Estudiante> Estudiantes { get; set; } = [];
        public List<Materia> Materias { get; set; } = [];
        public List<Colegiatura> Colegiaturas { get; set; } = [];
        public List<MaterialAdeudo> MaterialAdeudos { get; set; } = [];
        public List<Materia_Estudiante> MateriasEstudiantes { get; set; } =[];
    }
}
