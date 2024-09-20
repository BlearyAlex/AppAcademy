using AppAcademy.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace AppAcademy.Application.Features.Ventas.Queries.GetAllVentas
{
    public class GetAllVentasListQueryHandler : IRequestHandler<GetAllVentasListQuery, List<GetAllVentasVm>>
    {
        private readonly IVentaRepository _repository;
        private readonly IMapper _mapper;

        public GetAllVentasListQueryHandler(IVentaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllVentasVm>> Handle(GetAllVentasListQuery request, CancellationToken cancellationToken)
        {
            var ventaList = await _repository.GetAllAsync();

            return _mapper.Map<List<GetAllVentasVm>>(ventaList);
        }
    }
}
