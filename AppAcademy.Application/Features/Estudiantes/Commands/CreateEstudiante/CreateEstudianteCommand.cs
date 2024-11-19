using AppAcademy.Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AppAcademy.Application.Features.Estudiantes.Commands.CreateEstudiante
{
    public class CreateEstudianteCommand : IRequest<string>
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? ImageFile { get; set; }
        public EstudianteEstado EstadoEstudiante { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
