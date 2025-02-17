using MediatR;

namespace AppAcademy.Application.Features.Proveedores.Commands.CreateProveedor
{
    public class CreateProveedorCommand : IRequest<string>
    {
        public string? Nombre { get; set; }
        public string? Color { get; set; }
        public string? Description { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
