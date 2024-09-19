using AppAcademy.Domain.Auth;
using AppAcademy.Domain.Enum;

namespace AppAcademy.Domain.ControlAcademia
{
    public class Colegiatura
    {
        public string ColegiaturaId { get; set; } = Guid.NewGuid().ToString();
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public ColegiaturaEstado EstadoColegiatura { get; set; }


        // Relaciones
        public Estudiante? Estudiante { get; set; }

        public User? User { get; set; }
    }
}
