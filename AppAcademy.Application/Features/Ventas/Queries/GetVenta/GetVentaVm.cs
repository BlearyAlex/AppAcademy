using AppAcademy.Domain.Enum;

namespace AppAcademy.Application.Features.Ventas.Queries.GetVenta
{
    public class GetVentaVm
    {
        public string VentaId { get; set; }
        public DateTime FechaCompra { get; set; }
        public string? ClienteId { get; set; }
        public string? UserId { get; set; }
        public string EstadoVenta { get; set; }
    }
}
