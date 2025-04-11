using AppAcademy.Application.Features.Students.Commands.CreateStudent;
using AppAcademy.Application.Features.Students.Commands.DeleteStudent;
using AppAcademy.Application.Features.Students.Queries.GetAllStudents;
using AppAcademy.Application.Features.Students.Queries.GetGanttData;
using AppAcademy.Application.Features.Students.Queries.GetStudent;
using AppAcademy.Domain.ControlAcademia;

namespace AppAcademy.Application.Contracts.Persistence.IControlAcademia
{
    public interface IStudentRepository : IAsyncRepository<Student>
    {
        Task<int> CreateStudent(CreateStudentCommand student, string userName);
        Task<bool> DeleteStudent(DeleteStudentCommand student, string userName);
        Task<List<GetAllStudentsVm>> GetAllStudentsWithCareers();
        Task<GetStudentVm> GetStudentsByIdWithCareer(int studentId);
        Task<List<GetStudentCardVm>> GetStudentCard();
        Task<bool> StudentTienePagosActivos(int studentId);
    }
}
