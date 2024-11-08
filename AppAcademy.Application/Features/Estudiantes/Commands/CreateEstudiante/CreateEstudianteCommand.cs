using AppAcademy.Domain.Enum;
using MediatR;

namespace AppAcademy.Application.Features.Estudiantes.Commands.CreateEstudiante
{
    public class CreateEstudianteCommand : IRequest<string>
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public EstudianteEstado EstadoEstudiante { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
