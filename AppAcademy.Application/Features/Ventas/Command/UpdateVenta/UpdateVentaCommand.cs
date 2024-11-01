using AppAcademy.Domain.Enum;
using MediatR;

namespace AppAcademy.Application.Features.Ventas.Command.UpdateVenta
{
    public class UpdateVentaCommand : IRequest
    {
        public string VentaId { get; set; }
        public DateTime? FechaCompra { get; set; }
        public VentaEstado EstadoVenta { get; set; }
        public TipoPagoEstado EstadoTipoPago { get; set; }
        public string? ClienteId { get; set; }
        public decimal Bruto { get; set; }
        public decimal? Descuento { get; set; }
        public decimal Neto { get; set; }
        public int TotalProductos { get; set; }

        public List<UpdateDetalleVentaModel> Productos { get; set; } = new List<UpdateDetalleVentaModel>();
    }
    public class UpdateDetalleVentaModel
    {
        public string? DetalleVentaId { get; set; }
        public int Cantidad { get; set; }
        public decimal Costo { get; set; }
        public string VentaId { get; set; }
        public string ProductoId { get; set; }
    }

}
