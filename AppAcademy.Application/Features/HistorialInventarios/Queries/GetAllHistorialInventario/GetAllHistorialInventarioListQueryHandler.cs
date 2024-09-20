using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Features.EntradasProductos.Queries.GetAllEntradas;
using AutoMapper;
using MediatR;

namespace AppAcademy.Application.Features.HistorialInventarios.Queries.GetAllHistorialInventario
{
    public class GetAllHistorialInventarioListQueryHandler : IRequestHandler<GetAllHistorialInventarioListQuery, List<GetAllHistorialInventarioVm>>
    {
        private readonly IHistorialInventarioRepository _repository;
        private readonly IMapper _mapper;

        public GetAllHistorialInventarioListQueryHandler(IHistorialInventarioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllHistorialInventarioVm>> Handle(GetAllHistorialInventarioListQuery request, CancellationToken cancellationToken)
        {
            var HistorialInvList = await _repository.GetAllAsync();

            return _mapper.Map<List<GetAllHistorialInventarioVm>>(HistorialInvList);
        }
    }
}
