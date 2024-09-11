namespace AppAcademy.Domain.Auth
{
    public class Rol
    {
        public string RolId { get; set; } = Guid.NewGuid().ToString();
        public string? NameRol { get; set; }
        public string? Description { get; set; }

        // Relaciones
        public List<Permiso> Permisos { get; set; } = [];
        public List<User> Users { get; set; } = [];
    }
}
