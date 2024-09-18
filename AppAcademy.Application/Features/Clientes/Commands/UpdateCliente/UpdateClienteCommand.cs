using MediatR;

namespace AppAcademy.Application.Features.Clientes.Commands.UpdateCliente
{
    public class UpdateClienteCommand : IRequest
    {
        public string ClienteId { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
    }
}
