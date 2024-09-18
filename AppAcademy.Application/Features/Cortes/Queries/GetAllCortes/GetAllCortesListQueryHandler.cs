using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Features.Productos.Queries.GetAllProductos;
using AutoMapper;
using MediatR;

namespace AppAcademy.Application.Features.Cortes.Queries.GetAllCortes
{
    public class GetAllCortesListQueryHandler : IRequestHandler<GetAllCortesListQuery, List<GetAllCortesVm>>
    {
        private readonly ICorteRepository _corteRepository;
        private readonly IMapper _mapper;

        public GetAllCortesListQueryHandler(ICorteRepository corteRepository, IMapper mapper)
        {
            _corteRepository = corteRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllCortesVm>> Handle(GetAllCortesListQuery request, CancellationToken cancellationToken)
        {
            var corteList = await _corteRepository.GetAllAsync();

            return _mapper.Map<List<GetAllCortesVm>>(corteList);
        }
    }
}
