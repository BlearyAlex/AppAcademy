using MediatR;

namespace AppAcademy.Application.Features.Categorias.Commands.UpdateCategoria
{
    public class UpdateCategoriaCommand : IRequest
    {
        public string CategoriaId { get; set; }
        public string? Nombre { get; set; }
        public string? Color { get; set; }
        public string? Description { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
