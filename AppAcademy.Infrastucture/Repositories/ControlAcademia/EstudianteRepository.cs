using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Domain.ControlAcademia;
using AppAcademy.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AppAcademy.Infrastucture.Repositories.ControlAcademia
{
    public class EstudianteRepository : AsyncRepository<Estudiante>, IEstudianteRepository
    {
        public EstudianteRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Estudiante> GetEstudianteWithColegiatura(string studentId) 
        {
            var student = await _dbContext.Estudiantes
                .Include(e => e.Colegiaturas)
                .FirstOrDefaultAsync(e => e.EstudianteId == studentId);

            if (student == null)
            {
                throw new KeyNotFoundException($"Estudiante con ID {studentId} no encontrado.");
            }

            return student;
        }
    }
}
