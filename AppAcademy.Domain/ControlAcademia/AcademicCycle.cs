namespace AppAcademy.Domain.ControlAcademia
{
    public class AcademicCycle
    {
        public int AcademicCycleId { get; set; }
        public int NumeroCiclo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public int CareerId { get; set; }
        public virtual Career Career { get; set; }
    }
}
