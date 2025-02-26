

namespace AppAcademy.Application.Features.Permissions.Queries.GetPermission
{
    public class GetPermissionVm
    {
        public int PermissionId { get; set; }
        public string Nombre { get; set; }  // Ejemplo: "Beca", "Descuento", "Permiso de ausencia"
        public string Descripcion { get; set; }
        public bool Activo { get; set; }  // TRUE si el permiso sigue vigente
    }
}
