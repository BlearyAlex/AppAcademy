using AppAcademy.Domain.Enum;
using MediatR;

namespace AppAcademy.Application.Features.DetallesCortes.Command.CreateDetalleCorte
{
    public class CreateDetalleCorteCommand : IRequest<string>
    {
        public TipoPago TipoPago { get; set; } = TipoPago.Efectivo;
        public decimal Monto { get; set; }

        public string? CorteId { get; set; }
        public string? VentaId { get; set; }
    }
}
