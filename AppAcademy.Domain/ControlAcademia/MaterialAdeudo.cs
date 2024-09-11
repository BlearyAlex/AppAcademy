using AppAcademy.Domain.Auth;

namespace AppAcademy.Domain.ControlAcademia
{
    public class MaterialAdeudo
    {
        public string MaterialAdeudoId { get; set; } = Guid.NewGuid().ToString();
        public string? Descripcion { get; set; }
        public decimal Monto { get; set; }
        public Estado EstadoMaterial { get; set; }

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
