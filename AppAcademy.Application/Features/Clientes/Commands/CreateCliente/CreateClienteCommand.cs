using MediatR;

namespace AppAcademy.Application.Features.Clientes.Commands.CreateCliente
{
    public class CreateClienteCommand : IRequest<string>
    {
        public string? NombreCompleto { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
    }
}
