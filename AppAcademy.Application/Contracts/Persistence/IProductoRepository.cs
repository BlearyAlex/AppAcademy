using AppAcademy.Application.Features.Productos.Queries.GetAllProductos;
using AppAcademy.Application.Features.Productos.Queries.GetProductById;
using AppAcademy.Domain.PuntoDeVenta;

namespace AppAcademy.Application.Contracts.Persistence
{
    public interface IProductoRepository : IAsyncRepository<Producto>
    {
        Task<List<GetAllProductosVm>> GetAllProductos();
        Task<List<Producto>> GetProductsByCategoria(string categoria);
        Task<GetProductByIdVm> GetProductById(string productoId);
        Task<List<Producto>> GetProductsByName(string producto);
        Task<bool> ProductoTieneVentasActivas(string productoId);

        // Metodo auxiliares
        Task<bool> DescontarStock(string productoId, int cantidad);
        Task<bool> AgregarStock(string productoId, int cantidad);
    }
}
