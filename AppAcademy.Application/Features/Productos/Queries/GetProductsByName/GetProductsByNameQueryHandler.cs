using AppAcademy.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace AppAcademy.Application.Features.Productos.Queries.GetProductsByName
{
    public class GetProductsByNameQueryHandler : IRequestHandler<GetProductsByNameQuery, List<GetProductsByNameVm>>
    {

        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;

        public GetProductsByNameQueryHandler(IProductoRepository productoRepository, IMapper mapper)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
        }

        public async Task<List<GetProductsByNameVm>> Handle(GetProductsByNameQuery request, CancellationToken cancellationToken)
        {
            var products = await _productoRepository.GetProductsByName(request._Producto);

            return _mapper.Map<List<GetProductsByNameVm>>(products);
        }
    }
}
