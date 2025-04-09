using AppAcademy.Application.ViewModel.Marca;
using AppAcademy.Domain.PuntoDeVenta;

namespace AppAcademy.Application.Contracts.Persistence
{
    public interface IMarcaRepository : IAsyncRepository<Marca>
    {
        Task<bool> MarcaTieneProductosActivos(string marcaId);
        Task<List<SalesEvolutionByMarca>> GetSalesEvolutionByMarca(DateTime startDate, DateTime endDate);
        Task<List<HighlightedMarca>> GetHighlightedMarcas(DateTime startDate, DateTime endDate);
        Task<List<SalesByMarca>> GetSalesByMarca(DateTime startDate, DateTime endDate);
    }
}
