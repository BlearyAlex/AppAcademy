using AppAcademy.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace AppAcademy.Application.Features.Productos.Queries.GetAllProductos
{
    public class GetAllProductosListQueryHandler : IRequestHandler<GetAllProductosListQuery, List<GetAllProductosVm>>
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;

        public GetAllProductosListQueryHandler(IProductoRepository productoRepository, IMapper mapper)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllProductosVm>> Handle(GetAllProductosListQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productoRepository.GetAllAsync();

            return _mapper.Map<List<GetAllProductosVm>>(productList);
        }
    }
}
