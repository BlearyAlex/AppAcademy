using AppAcademy.Domain.Enum;
using MediatR;

namespace AppAcademy.Application.Features.DetallesCortes.Command.UpdateDetalleCorte
{
    public class UpdateDetalleCorteCommand : IRequest
    {
        public string DetalleCorteId { get; set; }
        public TipoPago TipoPago { get; set; }
        public decimal Monto { get; set; }

        public string? Corte { get; set; }
        public string? Venta { get; set; }
    }
}
