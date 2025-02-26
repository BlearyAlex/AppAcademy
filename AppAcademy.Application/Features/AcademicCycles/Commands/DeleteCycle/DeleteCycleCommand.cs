using MediatR;

namespace AppAcademy.Application.Features.AcademicCycles.Commands.DeleteCycle
{
    public class DeleteCycleCommand : IRequest
    {
        public int AcademicCycleId { get; set; }
    }
}
