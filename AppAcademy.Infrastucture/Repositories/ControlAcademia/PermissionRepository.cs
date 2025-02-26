using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Domain.ControlAcademia;
using AppAcademy.Infrastucture.Persistence;

namespace AppAcademy.Infrastucture.Repositories.ControlAcademia
{
    public class PermissionRepository : AsyncRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
