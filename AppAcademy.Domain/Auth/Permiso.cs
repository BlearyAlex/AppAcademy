namespace AppAcademy.Domain.Auth
{
    public class Permiso
    {
        public string PermisoId { get; set; } = Guid.NewGuid().ToString();
        public string? NamePermiso { get; set; }
        public string? Description { get; set; }

        // Relaciones
        public List<Rol> Roles { get; set; } = [];
    }
}
