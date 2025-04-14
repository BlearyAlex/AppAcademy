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
        public GetAllCyclesStudentsVm AcademicCycle { get; set; }
        public GetAllCareerStudentsVm Career { get; set; }
    }

    public class GetAllCareerStudentsVm
    {
        public int CareerId { get; set; }
        public string Nombre { get; set; }
        public string Color { get; set; }
    }

    public class GetAllCyclesStudentsVm
    {
        public int AcademicCycleId { get; set; }
        public string CicloAcademico { get; set; }
        public string Color { get; set; }
    }
}
