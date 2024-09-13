using MediatR;

namespace AppAcademy.Application.Features.Productos.Queries.GetProductById
{
    public class GetProductQuery : IRequest<GetProductByIdVm>
    {
        public string _ProductoId { get; set; }

        public GetProductQuery(string productoId)
        {
            _ProductoId = productoId;
        }
    }
}
