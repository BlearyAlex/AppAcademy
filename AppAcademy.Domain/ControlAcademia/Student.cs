using AppAcademy.Domain.Enum;

namespace AppAcademy.Domain.ControlAcademia
{
    public class Student
    {
        public string EstudianteId { get; set; } = Guid.NewGuid().ToString();
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Direccion { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime FechaIngreso { get; set; }
        public EstudianteEstado EstadoEstudiante { get; set; }

        public string CareerId { get; set; }
        public virtual Career Career { get; set; }

        public virtual List<Payment> Payments { get; set; }
        public virtual List<Permission> Permissions { get; set; }
    }
}
