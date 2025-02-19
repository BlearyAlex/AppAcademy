using AppAcademy.Domain.Enum;
using MediatR;

namespace AppAcademy.Application.Features.Ventas.Command.CreateVenta
{
    public class CreateVentaCommand : IRequest<string>
    {
        public string ClienteId { get; set; }
        public List<VentaDetalleDto> Detalles { get; set; }
        public decimal Descuento { get; set; }
        public decimal Impuesto { get; set; }
    }

    public class VentaDetalleDto
    {
        public string ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
