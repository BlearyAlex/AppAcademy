using AppAcademy.Application.Features.Ventas.Queries.GetAllVentas;
using AppAcademy.Domain.Enum;
using AppAcademy.Domain.PuntoDeVenta;

namespace AppAcademy.Application.Features.Ventas.Queries.GetVenta
{
    public class GetVentaVm
    {
        public string VentaId { get; set; }
        public DateTime? Fecha { get; set; }
        public string EstadoVenta { get; set; }
        public decimal SaldoPendiente { get; set; }
        public string? ClienteId { get; set; }
        public decimal? Descuento { get; set; }
        public decimal Total { get; set; }
        public decimal Impuesto { get; set; }
        public string Folio { get; set; }

        public List<GetVentaDetalleVm> VentaDetalle { get; set; }
        public List<GetAbonoVm> Abonos { get; set; }
    }
    public class GetVentaDetalleVm
    {
        public string VentaDetalleId { get; set; }
        public string VentaId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }
        public GetProductoVm Producto { get; set; }
    }

    public class GetAbonoVm
    {
        public int AbonoId { get; set; }
        public string VentaId { get; set; } 
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class GetProductoVm
    {
        public string ProductoId { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public decimal Precio { get; set; }
    }


}
