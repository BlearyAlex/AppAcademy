using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AutoMapper;
using MediatR;

namespace AppAcademy.Application.Features.AcademicCycles.Queries.GetCycle
{
    public class GetCycleQueryHandler : IRequestHandler<GetCycleQuery, GetCycleVm>
    {
        private readonly IAcademicCycleRepository _repository;
        private readonly IMapper _mapper;

        public GetCycleQueryHandler(IAcademicCycleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetCycleVm> Handle(GetCycleQuery request, CancellationToken cancellationToken)
        {
            var cycle = await _repository.GetCycleWithCareer(request._AcademicCycleId);

            return _mapper.Map<GetCycleVm>(cycle);
        }
    }
}
