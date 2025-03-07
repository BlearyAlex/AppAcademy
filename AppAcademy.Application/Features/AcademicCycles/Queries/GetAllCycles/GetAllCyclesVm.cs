namespace AppAcademy.Application.Features.AcademicCycles.Queries.GetAllCycles
{
    public class GetAllCyclesVm
    {
        public int AcademicCycleId { get; set; }
        public int NumeroCiclo { get; set; }
        public string Color { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public GetCareerCycleVm Career { get; set; }
    }

    public class GetCareerCycleVm
    {
        public int CareerId { get; set; }
        public string Nombre { get; set; }
        public string Color { get; set; }
    }
}
