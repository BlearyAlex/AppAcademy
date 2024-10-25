using AppAcademy.Domain.Enum;
using MediatR;

namespace AppAcademy.Application.Features.Ventas.Command.CreateVenta
{
    public class CreateVentaCommand : IRequest<string>
    {
        public DateTime? FechaCompra { get; set; }
        public VentaEstado EstadoVenta { get; set; }
        public string? ClienteId { get; set; }
        public decimal Bruto { get; set; }

        public List<CreateDetalleVentaModel>? Productos { get; set; } = new List<CreateDetalleVentaModel>();
    }

    public class CreateDetalleVentaModel
    {
        public TipoPagoEstado EstadoTipoPago { get; set; }
        public decimal Costo { get; set; }
        public CorteEstado EstadoCorte { get; set; }
        public int Cantidad { get; set; }
        public string? ProductoId { get; set; }
    }
}
