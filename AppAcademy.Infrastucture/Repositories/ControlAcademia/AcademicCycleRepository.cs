using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Domain.ControlAcademia;
using AppAcademy.Infrastucture.Persistence;

namespace AppAcademy.Infrastucture.Repositories.ControlAcademia
{
    public class AcademicCycleRepository : AsyncRepository<AcademicCycle>, IAcademicCycleRepository
    {
        public AcademicCycleRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
