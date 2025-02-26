using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Domain.ControlAcademia;
using AppAcademy.Infrastucture.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Infrastucture.Repositories.ControlAcademia
{
    public class StudentPermissionRepository : AsyncRepository<StudentPermission>, IStudentPermissionRepository
    {
        public StudentPermissionRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
