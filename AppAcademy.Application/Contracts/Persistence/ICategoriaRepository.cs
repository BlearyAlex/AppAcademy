using AppAcademy.Application.DTOs.Venta;
using AppAcademy.Application.ViewModel.Categoria;
using AppAcademy.Domain.PuntoDeVenta;

namespace AppAcademy.Application.Contracts.Persistence
{
    public interface ICategoriaRepository : IAsyncRepository<Categoria>
    {
        Task<bool> CategoriaTieneProductosActivos(string categoriaId);
        Task<List<SalesEvolutionByCategory>> GetSalesEvolutionByCategory(DateTime startDate, DateTime endDate);
        Task<List<HighlightedCategory>> GetHighlightedCategories(DateTime startDate, DateTime endDate);
        Task<List<SalesByCategory>> GetSalesByCategory(DateTime startDate, DateTime endDate);
    }
}
