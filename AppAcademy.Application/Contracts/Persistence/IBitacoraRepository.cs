using AppAcademy.Domain.Logs;

namespace AppAcademy.Application.Contracts.Persistence
{
    public interface IBitacoraRepository : IAsyncRepository<Bitacora>
    {
        Task<List<Bitacora>> GetAllBitacoras();
    }
}
