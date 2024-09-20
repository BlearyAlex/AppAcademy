using AppAcademy.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace AppAcademy.Application.Features.Salidas.Queries.GetAllSalidas
{
    public class GetAllSalidasListQueryHandler : IRequestHandler<GetAllSalidasListQuery, List<GetAllSalidasVm>>
    {
        private readonly ISalidaRepository _salidaRepository;
        private readonly IMapper _mapper;

        public GetAllSalidasListQueryHandler(ISalidaRepository salidaRepository, IMapper mapper)
        {
            _salidaRepository = salidaRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllSalidasVm>> Handle(GetAllSalidasListQuery request, CancellationToken cancellationToken)
        {
            var proveedorList = await _salidaRepository.GetAllAsync();

            return _mapper.Map<List<GetAllSalidasVm>>(proveedorList);
        }
    }
}
