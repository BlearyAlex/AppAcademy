using MediatR;

namespace AppAcademy.Application.Features.DetallesPagos.Queries.GetAllDetallesPagos
{
    public class GetAllDetallesPagosListQuery : IRequest<List<GetAllDetallesPagosVm>>
    {
    }
}
