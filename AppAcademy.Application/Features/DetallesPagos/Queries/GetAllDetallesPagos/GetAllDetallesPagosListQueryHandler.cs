using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Features.DetallesCortes.Queries.GetAllDetallesCortes;
using AutoMapper;
using MediatR;

namespace AppAcademy.Application.Features.DetallesPagos.Queries.GetAllDetallesPagos
{
    public class GetAllDetallesPagosListQueryHandler : IRequestHandler<GetAllDetallesPagosListQuery, List<GetAllDetallesPagosVm>>
    {
        private readonly IDetallePagoRepository _repository;
        private readonly IMapper _mapper;

        public GetAllDetallesPagosListQueryHandler(IDetallePagoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllDetallesPagosVm>> Handle(GetAllDetallesPagosListQuery request, CancellationToken cancellationToken)
        {
            var detallePagoList = await _repository.GetAllAsync();

            return _mapper.Map<List<GetAllDetallesPagosVm>>(detallePagoList);
        }
    }
}
