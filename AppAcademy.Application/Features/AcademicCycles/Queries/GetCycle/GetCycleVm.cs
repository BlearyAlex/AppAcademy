namespace AppAcademy.Application.Features.AcademicCycles.Queries.GetCycle
{
    public class GetCycleVm
    {
        public int AcademicCycleId { get; set; }
        public int NumeroCiclo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public int? CareerId { get; set; }
    }
}
