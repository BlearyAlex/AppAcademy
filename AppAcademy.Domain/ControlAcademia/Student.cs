using AppAcademy.Domain.Enum;

namespace AppAcademy.Domain.ControlAcademia
{
    public class Student
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

        public int? CareerId { get; set; }
        public virtual Career Career { get; set; }

        public int? AcademicCycleId { get; set; }
        public virtual AcademicCycle AcademicCycle { get; set; }

        public virtual List<Payment> Payments { get; set; }
        public virtual List<Permission> Permissions { get; set; }
    }
}
