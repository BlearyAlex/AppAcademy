using MediatR;

namespace AppAcademy.Application.Features.Productos.Queries.GetProductsByName
{
    public class GetProductByCategoriaQuery : IRequest<List<GetProductsByCategoriaVm>>
    {
        public string _Categoria { get; set; }

        public GetProductByCategoriaQuery(string categoria)
        {
            _Categoria = categoria;
        }
    }
}
