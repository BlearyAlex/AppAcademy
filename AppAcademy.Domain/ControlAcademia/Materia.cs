using AppAcademy.Domain.Auth;

namespace AppAcademy.Domain.ControlAcademia
{
    public class Materia
    {
        public string MateriaId { get; set; } = Guid.NewGuid().ToString();
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }

        // Relaciones
        public List<Materia_Estudiante> MateriasEstudiantes { get; set; } = [];

        public User? User { get; set; }
    }
}
