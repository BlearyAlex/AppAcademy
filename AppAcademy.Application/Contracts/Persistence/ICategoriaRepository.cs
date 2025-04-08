using AppAcademy.Application.ViewModel;
using AppAcademy.Domain.PuntoDeVenta;

namespace AppAcademy.Application.Contracts.Persistence
{
    public interface ICategoriaRepository : IAsyncRepository<Categoria>
    {
        Task<bool> CategoriaTieneProductosActivos(string categoriaId);
        Task<List<SalesEvolutionByCategory>> GetSalesEvolutionByCategory(DateTime startDate, DateTime endDate);
    }
}
