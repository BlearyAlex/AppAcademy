namespace AppAcademy.Domain.ControlAcademia
{
    public class Permission
    {
        public int PermissionId { get; set; }
        public string Nombre { get; set; }  // Ejemplo: "Beca", "Descuento", "Permiso de ausencia"
        public string Descripcion { get; set; }
        public bool Activo { get; set; }  // TRUE si el permiso sigue vigente

        public virtual List<StudentPermission> StudentPermissions { get; set; }
    }
}
