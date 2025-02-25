namespace AppAcademy.Domain.ControlAcademia
{
    public class StudentPermission
    {
        public int StudentPermissionId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public int? StudentId { get; set; }
        public virtual Student Student { get; set; }
        public int? PermissionId { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
