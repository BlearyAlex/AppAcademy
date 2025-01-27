using AppAcademy.Domain.Enum;

namespace AppAcademy.Domain.ControlAcademia
{
    public class Estudiante
    {
        public string EstudianteId { get; set; } = Guid.NewGuid().ToString();
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? ImageUrl { get; set; }
        public EstudianteEstado EstadoEstudiante { get; set; }
        public DateTime FechaNacimiento { get; set; }


        // Relaciones
        public List<Colegiatura> Colegiaturas { get; set; } = [];
        public List<MaterialAdeudo> MaterialAdeudos { get; set; } = [];
        public List<Materia_Estudiante> MateriaSEstudiantes { get; set; } = [];
    }
}
