using MediatR;

namespace AppAcademy.Application.Features.Productos.Queries.GetProductsByName
{
    public class GetProductsByNameQuery : IRequest<List<GetProductsByNameVm>>
    {
        public string _Producto { get; set; }

        public GetProductsByNameQuery(string producto)
        {
            _Producto = producto;
        }

    }
}
