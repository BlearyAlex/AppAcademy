using MediatR;

namespace AppAcademy.Application.Features.AcademicCycles.Commands.CreateCycle
{
    public class CreateCycleCommand : IRequest<int>
    {
        public int AcademicCycleId { get; set; }
        public int NumeroCiclo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public int? CareerId { get; set; }
    }
}
