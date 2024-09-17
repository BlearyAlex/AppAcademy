using MediatR;

namespace AppAcademy.Application.Features.Categorias.Commands.DeleteCategoria
{
    public class DeleteCategoriaCommand : IRequest
    {
        public string CategoriaId { get; set; }
    }
}
