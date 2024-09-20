using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Features.Productos.Queries.GetAllProductos;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Promociones.Queries.GetAllPromociones
{
    public class GetAllPromocionesListQueryHandler : IRequestHandler<GetAllPromocionesListQuery, List<GetAllPromocionesVm>>
    {
        private readonly IPromocionRepository _promocionRepository;
        private readonly IMapper _mapper;

        public GetAllPromocionesListQueryHandler(IPromocionRepository promocionRepository, IMapper mapper)
        {
            _promocionRepository = promocionRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllPromocionesVm>> Handle(GetAllPromocionesListQuery request, CancellationToken cancellationToken)
        {
            var promocionList = await _promocionRepository.GetAllAsync();

            return _mapper.Map<List<GetAllPromocionesVm>>(promocionList);
        }
    }
}
