using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AutoMapper;
using MediatR;

namespace AppAcademy.Application.Features.Careers.Queries.GetCareer
{
    public class GetCareerQueryHandler : IRequestHandler<GetCareerQuery, GetCareerVm>
    {
        private readonly ICareerRepository _careerRepository;
        private readonly IMapper _mapper;

        public GetCareerQueryHandler(ICareerRepository careerRepository, IMapper mapper)
        {
            _careerRepository = careerRepository;
            _mapper = mapper;
        }

        public async Task<GetCareerVm> Handle(GetCareerQuery request, CancellationToken cancellationToken)
        {
            var career = await _careerRepository.GetByIdInt(request._CareerId);

            return _mapper.Map<GetCareerVm>(career);
        }
    }
}
