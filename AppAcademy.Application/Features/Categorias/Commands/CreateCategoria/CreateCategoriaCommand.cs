using MediatR;

namespace AppAcademy.Application.Features.Categorias.Commands.CreateCategoria
{
    public class CreateCategoriaCommand : IRequest<string>
    {
        public string? Nombre { get; set; }
        public string? Color { get; set; }
        public string? Description { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
