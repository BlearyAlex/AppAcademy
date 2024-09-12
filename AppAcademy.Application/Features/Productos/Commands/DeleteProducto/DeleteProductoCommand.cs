using MediatR;

namespace AppAcademy.Application.Features.Productos.Commands.DeleteProducto
{
    public class DeleteProductoCommand : IRequest
    {
       public string ProductoId { get; set; }
    }
}
