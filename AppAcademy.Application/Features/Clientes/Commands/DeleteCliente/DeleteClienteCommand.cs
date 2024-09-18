using MediatR;

namespace AppAcademy.Application.Features.Clientes.Commands.DeleteCliente
{
    public class DeleteClienteCommand : IRequest
    {
        public string ClienteId { get; set; }
    }
}
