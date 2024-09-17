using MediatR;

namespace AppAcademy.Application.Features.Proveedores.Commands.DeleteProveedor
{
    public class DeleteProveedorCommand : IRequest
    {
        public string ProveedorId { get; set; }
    }
}
