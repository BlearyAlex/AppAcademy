using MediatR;

namespace AppAcademy.Application.Features.Categorias.Queries.GetAllCategoria
{
    public class GetAllCategoriasListQuery : IRequest<List<GetAllCategoriasVm>>
    {
    }
}
