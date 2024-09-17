using MediatR;

namespace AppAcademy.Application.Features.Proveedores.Commands.UpdateProveedor
{
    public class UpdateProveedorCommand : IRequest
    {
        public string ProveedorId { get; set; }
        public string? Nombre { get; set; }
    }
}
