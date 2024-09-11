using AppAcademy.Domain.Auth;

namespace AppAcademy.Domain.ControlAcademia
{
    public class Materia_Estudiante
    {
        public string MateriaEstudianteId { get; set; } = Guid.NewGuid().ToString();
        public int Faltas { get; set; } = 0;

        // Relaciones
        public Estudiante? Estudiante { get; set; }
        public Materia? Materia { get; set; }

        public User? User { get; set; }
    }
}
