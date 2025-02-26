using MediatR;

namespace AppAcademy.Application.Features.AcademicCycles.Queries.GetCycle
{
    public class GetCycleQuery : IRequest<GetCycleVm>
    {
        public int _AcademicCycleId { get; set; }

        public GetCycleQuery(int academicCycleId)
        {
            _AcademicCycleId = academicCycleId;
        }
    }
}
