using AppAcademy.Domain.PuntoDeVenta;

namespace AppAcademy.Application.Contracts.Persistence
{
    public interface IEntradaRepository : IAsyncRepository<Entrada>
    {
        Task CreateEntradaWithProduct(Entrada nuevaEntrada, List<EntradaProducto> productos);
        Task<List<Entrada>> GetEntradasWithProductos();
        Task<Entrada> ObtenerEntradaPorId(string entradaId);
        Task UpdateEntradaWithProducts(Entrada entradaEditada, List<EntradaProducto> productosActualizados);
        Task DeleteEntrada(string entradaId);
    }
}
