using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Application.Features.Students.Queries.GetAllStudents;
using AppAcademy.Application.Features.Students.Queries.GetStudent;
using AppAcademy.Domain.ControlAcademia;
using AppAcademy.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AppAcademy.Infrastucture.Repositories.ControlAcademia
{
    public class StudentRepository : AsyncRepository<Student>, IStudentRepository
    {
        public StudentRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<GetAllStudentsVm>> GetAllStudentsWithCareers()
        {
            try
            {
                var students = await _dbContext.Students
                    .Include(s => s.Career)
                    .Select(s => new GetAllStudentsVm
                    {
                        StudentId = s.StudentId,
                        Nombre = s.Nombre,
                        Apellido = s.Apellido,
                        Telefono = s.Telefono,
                        Email = s.Email,
                        Direccion = s.Direccion,
                        ImageUrl = s.ImageUrl,
                        FechaIngreso = s.FechaIngreso,
                        EstadoEstudiante = s.EstadoEstudiante.ToString(),
                        Career = new GetAllCareerVm
                        {
                            CareerId = s.Career.CareerId,
                            Nombre = s.Career.Nombre
                        }
                    }).ToListAsync();

                return students;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<GetStudentVm> GetStudentsByIdWithCareer(int studentId)
        {
            var student = await _dbContext.Students
                .Include(s => s.Career)
                .Where(s => s.StudentId == studentId)
                .Select(s => new GetStudentVm
                {
                    StudentId = s.StudentId,
                    Nombre = s.Nombre,
                    Apellido = s.Apellido,
                    Telefono = s.Telefono,
                    Email = s.Email,
                    Direccion = s.Direccion,
                    ImageUrl = s.ImageUrl,
                    FechaIngreso = s.FechaIngreso,
                    EstadoEstudiante = s.EstadoEstudiante,
                    Career = new GetCareerById
                    {
                        CareerId = s.Career.CareerId,
                        Nombre = s.Career.Nombre
                    }
                }).FirstOrDefaultAsync();

            return student;
        }
    }
}
