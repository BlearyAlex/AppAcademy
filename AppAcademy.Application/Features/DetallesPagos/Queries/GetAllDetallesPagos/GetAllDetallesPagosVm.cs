using AppAcademy.Domain.Enum;

namespace AppAcademy.Application.Features.DetallesPagos.Queries.GetAllDetallesPagos
{
    public class GetAllDetallesPagosVm
    {
        public TipoPago Tipo { get; set; }
        public decimal Monto { get; set; }
    }
}
