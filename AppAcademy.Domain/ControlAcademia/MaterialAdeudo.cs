using AppAcademy.Domain.Auth;
using AppAcademy.Domain.Enum;

namespace AppAcademy.Domain.ControlAcademia
{
    public class MaterialAdeudo
    {
        public string MaterialAdeudoId { get; set; } = Guid.NewGuid().ToString();
        public string? Descripcion { get; set; }
        public decimal Monto { get; set; }
        public ColegiaturaEstado EstadoMaterial { get; set; }


        // Relaciones
        public Estudiante? Estudiante { get; set; }

        public User? User { get; set; }
    }
}
