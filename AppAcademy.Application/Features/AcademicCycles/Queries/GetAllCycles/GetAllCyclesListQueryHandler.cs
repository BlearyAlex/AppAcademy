using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.AcademicCycles.Queries.GetAllCycles
{
    public class GetAllCyclesListQueryHandler : IRequestHandler<GetAllCyclesListQuery, List<GetAllCyclesVm>>
    {
        private readonly IAcademicCycleRepository _academicCycleRepository;
        private readonly ILogger<GetAllCyclesListQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllCyclesListQueryHandler(IAcademicCycleRepository academicCycleRepository, ILogger<GetAllCyclesListQueryHandler> logger, IMapper mapper)
        {
            _academicCycleRepository = academicCycleRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<GetAllCyclesVm>> Handle(GetAllCyclesListQuery request, CancellationToken cancellationToken)
        {
            var cyclesList = await _academicCycleRepository.GetAllCyclesWithCareer();

            return _mapper.Map<List<GetAllCyclesVm>>(cyclesList);
        }
    }
}
