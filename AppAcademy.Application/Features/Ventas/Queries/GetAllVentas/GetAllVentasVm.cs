using AppAcademy.Application.Features.Ventas.Command.CreateVenta;
using AppAcademy.Domain.Enum;

namespace AppAcademy.Application.Features.Ventas.Queries.GetAllVentas
{
    public class GetAllVentasVm
    {
        public string VentaId { get; set; }
        public DateTime? Fecha { get; set; }
        public string EstadoVenta { get; set; }
        public decimal SaldoPendiente { get; set; }
        public GetAllVentasClient Cliente { get; set; }
        public decimal? Descuento { get; set; }
        public decimal Total { get; set; }
        public decimal Impuesto { get; set; }
        public string Folio { get; set; }
    }

    public class GetAllVentasClient
    {
        public string ClienteId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
    }

}
