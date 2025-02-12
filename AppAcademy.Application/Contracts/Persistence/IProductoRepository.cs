using AppAcademy.Application.Features.Productos.Queries.GetAllProductos;
using AppAcademy.Application.Features.Productos.Queries.GetProductById;
using AppAcademy.Application.Features.Productos.Queries.GetProductsMostSale;
using AppAcademy.Domain.PuntoDeVenta;

namespace AppAcademy.Application.Contracts.Persistence
{
    public interface IProductoRepository : IAsyncRepository<Producto>
    {
        Task<List<GetAllProductosVm>> GetAllProductos();
        Task<List<Producto>> GetProductsByCategoria(string categoria);
        Task<GetProductByIdVm> GetProductById(string productoId);
        Task<List<Producto>> GetProductsByName(string producto);
        Task<List<GetProductsMostSaleVm>> GetProductsMostSale(CancellationToken cancellationToken);
    }
}
