using AppAcademy.Domain.Enum;

namespace AppAcademy.Application.Features.Students.Queries.GetStudent
{
    public class GetStudentVm
    {
        public int StudentId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Direccion { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime FechaIngreso { get; set; }
        public EstudianteEstado EstadoEstudiante { get; set; }
        public GetCycleStudentById AcademicCycle { get; set; }
        public GetCareerStudentById Career { get; set; }
    }

    public class GetCareerStudentById
    {
        public int CareerId { get; set; }
        public string Nombre { get; set; }
        public string Color { get; set; }
    }

    public class GetCycleStudentById
    {
        public int AcademicCycleId { get; set; }
        public string CicloAcademico { get; set; }
        public string Color { get; set; }
    }
}
