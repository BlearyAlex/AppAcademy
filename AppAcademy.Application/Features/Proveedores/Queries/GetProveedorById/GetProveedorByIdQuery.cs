using MediatR;

namespace AppAcademy.Application.Features.Proveedores.Queries.GetProveedorById
{
    public class GetProveedorByIdQuery : IRequest<GetProveedorByIdVm>
    {
        public string _ProveedorId { get; set; }

        public GetProveedorByIdQuery(string proveedorId)
        {
            _ProveedorId = proveedorId;
        }
    }
}
