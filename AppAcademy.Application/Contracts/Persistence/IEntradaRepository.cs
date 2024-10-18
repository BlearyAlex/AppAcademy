using AppAcademy.Domain.PuntoDeVenta;

namespace AppAcademy.Application.Contracts.Persistence
{
    public interface IEntradaRepository : IAsyncRepository<Entrada>
    {
        Task<string> CreateEntradaWithProduct(Entrada nuevaEntrada);
        Task<List<Entrada>> GetEntradasWithProductos();
        Task<Entrada> GetEntradaByIdWithProductsAsync(string entradaId);
        Task DeleteEntrada(string entradaId);
        Task DeleteProductoAsync(EntradaProducto producto);
    }
}
