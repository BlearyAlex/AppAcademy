using AppAcademy.Domain.Enum;

namespace AppAcademy.Application.Features.Students.Queries.GetAllStudents
{
    public class GetAllStudentsVm
    {
        public int StudentId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Direccion { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string EstadoEstudiante { get; set; }

        public int CareerId { get; set; }
    }
}
