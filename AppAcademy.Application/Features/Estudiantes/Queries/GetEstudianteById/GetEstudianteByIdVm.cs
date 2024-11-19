using AppAcademy.Domain.Enum;

namespace AppAcademy.Application.Features.Estudiantes.Queries.GetEstudianteById
{
    public class GetEstudianteByIdVm
    {
        public string EstudianteId { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        public string Telefono { get; set; }
        public string? Direccion { get; set; }
        public string ImageUrl { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public EstudianteEstado EstadoEstudiante { get; set; }
    }
}
