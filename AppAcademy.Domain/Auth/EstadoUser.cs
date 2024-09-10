namespace AppAcademy.Domain.Auth
{
    public class EstadoUser
    {
        public string EstadoUserId { get; set; } = Guid.NewGuid().ToString();
        public Estado UserEstado { get; set; }

        // Definición del enum dentro de la clase
        public enum Estado
        {
            Activo,
            Inactivo,
            Pendiente,
            Suspendido
        }

        // Relaciones
        public IList<User> Users { get; set; } = new List<User>();
    }
}
