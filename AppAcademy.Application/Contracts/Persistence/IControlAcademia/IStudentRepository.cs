using AppAcademy.Application.Features.Students.Queries.GetAllStudents;
using AppAcademy.Application.Features.Students.Queries.GetStudent;
using AppAcademy.Domain.ControlAcademia;

namespace AppAcademy.Application.Contracts.Persistence.IControlAcademia
{
    public interface IStudentRepository : IAsyncRepository<Student>
    {
        Task<List<GetAllStudentsVm>> GetAllStudentsWithCareers();
        Task<GetStudentVm> GetStudentsByIdWithCareer(int studentId);
    }
}
