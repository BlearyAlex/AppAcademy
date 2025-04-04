using AppAcademy.Domain.PuntoDeVenta;

namespace AppAcademy.Application.Contracts.Persistence
{
    public interface IMarcaRepository : IAsyncRepository<Marca>
    {
        Task<bool> MarcaTieneProductosActivos(string marcaId);
    }
}
