using MediatR;

namespace AppAcademy.Application.Features.Ventas.Queries.GetVenta
{
    public class GetVentaQuery : IRequest<GetVentaVm>
    {
        public string _VentaId { get; set; }
        public GetVentaQuery(string ventaId)
        {
            _VentaId = ventaId;
        }
    }
}
