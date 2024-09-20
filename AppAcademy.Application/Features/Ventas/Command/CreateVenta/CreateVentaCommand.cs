using MediatR;

namespace AppAcademy.Application.Features.Ventas.Command.CreateVenta
{
    public class CreateVentaCommand : IRequest<string>
    {
        public DateTime FechaCompra { get; set; }

        // Relaciones
        public string? ClienteId { get; set; }
        public string? UserId { get; set; }
    }
}
