using MediatR;

namespace AppAcademy.Application.Features.Productos.Queries.GetProductsMostSale
{
    public class GetProductsMostSaleQuery : IRequest<List<GetProductsMostSaleVm>>
    {
    }
}
