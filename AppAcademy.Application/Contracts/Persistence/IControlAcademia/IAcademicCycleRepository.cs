using AppAcademy.Application.Features.AcademicCycles.Queries.GetAllCycles;
using AppAcademy.Application.Features.AcademicCycles.Queries.GetCycle;
using AppAcademy.Domain.ControlAcademia;

namespace AppAcademy.Application.Contracts.Persistence.IControlAcademia
{
    public interface IAcademicCycleRepository : IAsyncRepository<AcademicCycle>
    {
        Task<List<GetAllCyclesVm>> GetAllCyclesWithCareer();
        Task<GetCycleVm> GetCycleWithCareer(int cycleId);
        Task<List<(int mes, int anio)>> GetMonthsAvailable(int studentId, AcademicCycle cycle);
    }
}
