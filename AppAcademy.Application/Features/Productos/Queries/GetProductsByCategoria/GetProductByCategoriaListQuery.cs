using MediatR;

namespace AppAcademy.Application.Features.Productos.Queries.GetProductsByName
{
    public class GetProductByCategoriaListQuery : IRequest<List<GetProductByCategoriaListQuery>>
    {
        public string _Categoria { get; set; }

        public GetProductByCategoriaListQuery(string categoria)
        {
            _Categoria = categoria;
        }
    }
}
