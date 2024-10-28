using AppAcademy.Application.Features.Ventas.Queries.GetAllVentas;
using AppAcademy.Domain.Enum;

namespace AppAcademy.Application.Features.Ventas.Queries.GetVenta
{
    public class GetVentaVm
    {
        public string VentaId { get; set; }
        public DateTime? FechaCompra { get; set; }
        public VentaEstado EstadoVenta { get; set; }
        public TipoPagoEstado EstadoTipoPago { get; set; }
        public string? ClienteId { get; set; }
        public decimal Bruto { get; set; }
        public int TotalProductos { get; set; }

        public List<GetDetallesVentaVm> Productos { get; set; }
    }
    public class GetDetallesVentaVm
    {
        public string DetalleVentaId { get; set; }
        public decimal Costo { get; set; }
        public int Cantidad { get; set; }
        public string ProductoId { get; set; }
    }

}
