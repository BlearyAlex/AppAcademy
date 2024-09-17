using MediatR;

namespace AppAcademy.Application.Features.Proveedores.Queries.GetAllProveedor
{
    public class GetAllProveedoresListQuery : IRequest<List<GetAllProveedoresVm>>
    {
    }
}
