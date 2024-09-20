using MediatR;

namespace AppAcademy.Application.Features.EntradasProductos.Queries.GetEntrada
{
    public class GetEntradaProductoQuery : IRequest<GetEntradaProductoVm>
    {
        public string _EntradaProductoId { get; set; }
    }
}
