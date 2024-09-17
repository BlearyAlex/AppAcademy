using AppAcademy.Domain.PuntoDeVenta;

namespace AppAcademy.Application.Contracts.Persistence
{
    public interface IProductoRepository : IAsyncRepository<Producto>
    {
        Task<List<Producto>> GetProductsByCategoria(string categoria);
        Task<List<Producto>> GetProductsByName(string producto);
    }
}
