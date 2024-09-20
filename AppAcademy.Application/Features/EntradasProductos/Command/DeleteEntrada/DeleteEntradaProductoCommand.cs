using MediatR;

namespace AppAcademy.Application.Features.EntradasProductos.Command.DeleteEntrada
{
    public class DeleteEntradaProductoCommand : IRequest
    {
        public string EntradaProductoId { get; set; }
    }
}
