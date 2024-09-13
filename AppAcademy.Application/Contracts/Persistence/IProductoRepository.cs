using AppAcademy.Application.Features.Productos.Queries.GetProductsByName;
using AppAcademy.Domain.PuntoDeVenta;

namespace AppAcademy.Application.Contracts.Persistence
{
    public interface IProductoRepository : IAsyncRepository<Producto>
    {
        Task<List<Producto>> GetProductsByCategoria(string categoria);
    }
}
