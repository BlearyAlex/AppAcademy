

namespace AppAcademy.Application.Features.Permissions.Queries.GetAllPermissions
{
    public class GetAllPermissionsVm
    {
        public int PermissionId { get; set; }
        public string Nombre { get; set; }  // Ejemplo: "Beca", "Descuento", "Permiso de ausencia"
        public string Descripcion { get; set; }
        public bool Activo { get; set; }  // TRUE si el permiso sigue vigente
    }
}
