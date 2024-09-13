using AppAcademy.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace AppAcademy.Application.Features.Productos.Queries.GetProductsByName
{
    public class GetProductByCategoriaQueryHandler : IRequestHandler<GetProductByCategoriaQuery, List<GetProductsByCategoriaVm>>
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;

        public GetProductByCategoriaQueryHandler(IProductoRepository productoRepository, IMapper mapper)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
        }

        public async Task<List<GetProductsByCategoriaVm>> Handle(GetProductByCategoriaQuery request, CancellationToken cancellationToken)
        {
            var productsList = await _productoRepository.GetProductsByCategoria(request._Categoria);

            return _mapper.Map<List<GetProductsByCategoriaVm>>(productsList);
        }
    }
}
