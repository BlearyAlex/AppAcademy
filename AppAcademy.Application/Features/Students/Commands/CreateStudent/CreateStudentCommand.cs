using AppAcademy.Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AppAcademy.Application.Features.Students.Commands.CreateStudent
{
    public class CreateStudentCommand : IRequest<int>
    {
        public int StudentId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Direccion { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? ImageFile { get; set; }
        public DateTime FechaIngreso { get; set; }
        public EstudianteEstado EstadoEstudiante { get; set; }

        public int? CareerId { get; set; }
    }
}
