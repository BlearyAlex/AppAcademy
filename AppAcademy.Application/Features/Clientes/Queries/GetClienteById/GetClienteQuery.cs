using MediatR;

namespace AppAcademy.Application.Features.Clientes.Queries.GetClienteById
{
    public class GetClienteQuery : IRequest<GetClienteVm>
    {
        public string _ClienteId { get; set; }

        public GetClienteQuery(string clienteId)
        {
            _ClienteId = clienteId;
        }
    }
}
