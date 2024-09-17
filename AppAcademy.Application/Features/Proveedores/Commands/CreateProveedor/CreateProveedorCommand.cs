using MediatR;

namespace AppAcademy.Application.Features.Proveedores.Commands.CreateProveedor
{
    public class CreateProveedorCommand : IRequest<string>
    {
        public string? Nombre { get; set; }
    }
}
