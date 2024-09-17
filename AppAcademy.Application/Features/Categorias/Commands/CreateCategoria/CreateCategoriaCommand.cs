using MediatR;

namespace AppAcademy.Application.Features.Categorias.Commands.CreateCategoria
{
    public class CreateCategoriaCommand : IRequest<string>
    {
        public string? Nombre { get; set; }
    }
}
