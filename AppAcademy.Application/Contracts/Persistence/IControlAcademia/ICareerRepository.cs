using AppAcademy.Domain.ControlAcademia;

namespace AppAcademy.Application.Contracts.Persistence.IControlAcademia
{
    public interface ICareerRepository : IAsyncRepository<Career>
    {
        Task<bool> CareerTienePagosActivos(int careerId);
        Task<List<Career>> GetAllCareerFilter();
    }
}
