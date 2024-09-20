using AppAcademy.Domain.Enum;
using MediatR;

namespace AppAcademy.Application.Features.DetallesPagos.Command.CreateDetallePago
{
    public class CreateDetallePagoCommand : IRequest<string>
    {
        public TipoPago Tipo { get; set; }
        public decimal Monto { get; set; }

        public string? VentaId { get; set; }
    }
}
