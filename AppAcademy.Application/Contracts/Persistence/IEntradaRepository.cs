using AppAcademy.Domain.PuntoDeVenta;

namespace AppAcademy.Application.Contracts.Persistence
{
    public interface IEntradaRepository : IAsyncRepository<Entrada>
    {
        Task<string> CreateEntradaWithProduct(Entrada nuevaEntrada);
        Task<List<Entrada>> GetEntradasWithProductos();
        Task<Entrada> ObtenerEntradaPorId(string entradaId);
        Task<Entrada> GetByIdWithProductsAsync(string entradaId);
        Task DeleteEntrada(string entradaId);
    }
}
