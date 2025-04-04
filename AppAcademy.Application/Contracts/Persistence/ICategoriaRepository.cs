using AppAcademy.Domain.PuntoDeVenta;

namespace AppAcademy.Application.Contracts.Persistence
{
    public interface ICategoriaRepository : IAsyncRepository<Categoria>
    {
        Task<bool> CategoriaTieneProductosActivos(string categoriaId);
    }
}
