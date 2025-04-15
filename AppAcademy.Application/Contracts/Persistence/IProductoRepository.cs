using AppAcademy.Application.Features.Productos.Queries.GetAllProductos;
using AppAcademy.Application.Features.Productos.Queries.GetAllProductosFilter;
using AppAcademy.Application.Features.Productos.Queries.GetProductById;
using AppAcademy.Application.ViewModel.Producto;
using AppAcademy.Domain.PuntoDeVenta;

namespace AppAcademy.Application.Contracts.Persistence
{
    public interface IProductoRepository : IAsyncRepository<Producto>
    {
        Task<List<GetAllProductosVm>> GetAllProductos();
        Task<List<GetAllProductosFilterVm>> GetAllProductosWithFilter();
        Task<List<Producto>> GetProductsByCategoria(string categoria);
        Task<GetProductByIdVm> GetProductById(string productoId);
        Task<List<Producto>> GetProductsByName(string producto);
        Task<bool> ProductoTieneVentasActivas(string productoId);

        // Direct Methods
        Task<List<SalesEvolutionByProducto>> GetSalesEvolutionByProducto(DateTime startDate, DateTime endDate);
        Task<List<HighlightedProducto>> GetHighlightedProducto(DateTime startDate, DateTime endDate);
        Task<List<SalesByProducto>> GetSalesByProducto(DateTime startDate, DateTime endDate);

        // Metodo auxiliares
        Task<bool> DescontarStock(string productoId, int cantidad);
        Task<bool> AgregarStock(string productoId, int cantidad);
        Task<bool> CleanImageProductAsync(string imageName);
    }
}
