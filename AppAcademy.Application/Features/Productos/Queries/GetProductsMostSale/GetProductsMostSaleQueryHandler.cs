using AppAcademy.Application.Contracts.Persistence;
using MediatR;

namespace AppAcademy.Application.Features.Productos.Queries.GetProductsMostSale
{
    public class GetProductsMostSaleQueryHandler : IRequestHandler<GetProductsMostSaleQuery, List<GetProductsMostSaleVm>>
    {
        private readonly IProductoRepository _productoRepository;

        public GetProductsMostSaleQueryHandler(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<List<GetProductsMostSaleVm>> Handle(GetProductsMostSaleQuery request, CancellationToken cancellationToken)
        {
            return await _productoRepository.GetProductsMostSale(cancellationToken);
        }
    }
}
