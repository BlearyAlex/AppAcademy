using MediatR;

namespace AppAcademy.Application.Features.Categorias.Queries.GetCategoriaById
{
    public class GetCategoriaByIdQuery : IRequest<GetCategoriaByIdVm>
    {
        public string _CategoriaId { get; set; }

        public GetCategoriaByIdQuery(string categoriaId)
        {
            _CategoriaId = categoriaId;
        }
    }
}
