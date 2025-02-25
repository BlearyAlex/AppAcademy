using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Application.Features.Categorias.Queries.GetAllCategoria;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Careers.Queries.GetAllCareers
{
    public class GetAllCareerListQueryHandler : IRequestHandler<GetAllCareerListQuery, List<GetAllCareersVm>>
    {
        private readonly ICareerRepository _careerRepository;
        private readonly ILogger<GetAllCategoriasListQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllCareerListQueryHandler(ICareerRepository careerRepository, ILogger<GetAllCategoriasListQueryHandler> logger, IMapper mapper)
        {
            _careerRepository = careerRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<GetAllCareersVm>> Handle(GetAllCareerListQuery request, CancellationToken cancellationToken)
        {
            var careerList = await _careerRepository.GetAllAsync();

            return _mapper.Map<List<GetAllCareersVm>>(careerList);
        }
    }
}
