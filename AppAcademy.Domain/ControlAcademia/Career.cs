namespace AppAcademy.Domain.ControlAcademia
{
    public class Career
    {
        public int CareerId { get; set; }
        public string Nombre { get; set; }
        public decimal CostoMensual { get; set; }
        public string? Color { get; set; }
        public bool Activa { get; set; }

        public virtual List<Student> Students { get; set; }
        public virtual List<AcademicCycle> AcademicCycles { get; set; }
    }
}
