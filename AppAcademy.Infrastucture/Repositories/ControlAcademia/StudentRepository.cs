using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Domain.ControlAcademia;
using AppAcademy.Infrastucture.Persistence;

namespace AppAcademy.Infrastucture.Repositories.ControlAcademia
{
    public class StudentRepository : AsyncRepository<Student>, IStudentRepository
    {
        public StudentRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
