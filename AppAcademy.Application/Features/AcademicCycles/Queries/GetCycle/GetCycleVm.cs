using AppAcademy.Application.Features.AcademicCycles.Queries.GetAllCycles;

namespace AppAcademy.Application.Features.AcademicCycles.Queries.GetCycle
{
    public class GetCycleVm
    {
        public int AcademicCycleId { get; set; }
        public string CicloAcademico { get; set; }
        public string Color { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public GetCareerCycleVm Career { get; set; }
    }
}
