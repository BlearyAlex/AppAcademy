using AppAcademy.Domain.Auth;

namespace AppAcademy.Domain.ControlAcademia
{
    public class Estudiante
    {
        public string EstudianteId { get; set; } = Guid.NewGuid().ToString();
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        public int Telefono { get; set; }
        public string? Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }

        // Relaciones
        public List<Colegiatura> Colegiaturas { get; set; } = [];
        public List<MaterialAdeudo> MaterialAdeudos { get; set; } = [];
        public List<Materia_Estudiante> MateriaSEstudiantes { get; set; } = [];

        public User? User { get; set; }
    }
}
