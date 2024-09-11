using AppAcademy.Domain.Auth;

namespace AppAcademy.Domain.ControlAcademia
{
    public class Colegiatura
    {
        public string ColegiaturaId { get; set; } = Guid.NewGuid().ToString();
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public Estado EstadoColegiatura { get; set; }

        public enum Estado
        {
            Pagado,
            Pendiente,
            Atrasado
        }

        // Relaciones
        public Estudiante? Estudiante { get; set; }

        public User? User { get; set; }
    }
}
