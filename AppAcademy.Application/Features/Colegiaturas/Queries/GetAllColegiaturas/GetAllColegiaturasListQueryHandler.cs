using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AutoMapper;
using MediatR;

namespace AppAcademy.Application.Features.Colegiaturas.Queries.GetAllColegiaturas
{
    public class GetAllColegiaturasListQueryHandler : IRequestHandler<GetAllColegiaturasListQuery, List<GetAllColegiaturasVm>>
    {
        private readonly IColegiaturaRepository _colegiaturaRepository;
        private readonly IMapper _mapper;

        public GetAllColegiaturasListQueryHandler(IColegiaturaRepository colegiaturaRepository, IMapper mapper)
        {
            _colegiaturaRepository = colegiaturaRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllColegiaturasVm>> Handle(GetAllColegiaturasListQuery request, CancellationToken cancellationToken)
        {
            var colegiaturaList = await _colegiaturaRepository.GetAllAsync();

            return _mapper.Map<List<GetAllColegiaturasVm>>(colegiaturaList);
        }
    }
}
