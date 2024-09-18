using MediatR;

namespace AppAcademy.Application.Features.Clientes.Queries.GetAllCliente
{
    public class GetAllClientesListQuery : IRequest<List<GetAllClientesVm>>
    {
    }
}
