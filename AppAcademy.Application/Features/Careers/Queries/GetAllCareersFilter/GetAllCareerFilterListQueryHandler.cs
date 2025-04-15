using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Careers.Queries.GetAllCareersFilter
{
    public class GetAllCareerFilterListQueryHandler : IRequestHandler<GetAllCareerFilterListQuery, List<GetAllCareerFilterVm>>
    {
        private readonly ICareerRepository _careerRepository;
        private readonly ILogger<GetAllCareerFilterListQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllCareerFilterListQueryHandler(ICareerRepository careerRepository, ILogger<GetAllCareerFilterListQueryHandler> logger, IMapper mapper)
        {
            _careerRepository = careerRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<GetAllCareerFilterVm>> Handle(GetAllCareerFilterListQuery request, CancellationToken cancellationToken)
        {
            var careerList = await _careerRepository.GetAllCareerFilter();

            return _mapper.Map<List<GetAllCareerFilterVm>>(careerList);
        }
    }
}
