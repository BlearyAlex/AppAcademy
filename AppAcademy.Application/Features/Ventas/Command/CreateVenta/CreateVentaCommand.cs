using AppAcademy.Domain.Enum;
using MediatR;

namespace AppAcademy.Application.Features.Ventas.Command.CreateVenta
{
    public class CreateVentaCommand : IRequest<string>
    {
        public DateTime? FechaCompra { get; set; }
        public VentaEstado EstadoVenta { get; set; }
        public TipoPagoEstado EstadoTipoPago { get; set; }
        public string? ClienteId { get; set; }
        public decimal Bruto { get; set; }
        public decimal? Descuento { get; set; }
        public decimal Neto { get; set; }
        public int TotalProductos { get; set; }

        public List<CreateDetalleVentaModel>? Productos { get; set; } = new List<CreateDetalleVentaModel>();
    }

    public class CreateDetalleVentaModel
    {
        public decimal Costo { get; set; }
        public int Cantidad { get; set; }
        public string ProductoId { get; set; }
    }
}
