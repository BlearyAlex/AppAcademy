using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Domain.ControlAcademia;
using AppAcademy.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace AppAcademy.Infrastucture.Repositories.ControlAcademia
{
    public class ColegiaturaRepository : AsyncRepository<Colegiatura>, IColegiaturaRepository
    {
        public ColegiaturaRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Colegiatura>> GetAllAsyncWithEstudiante()
        {
            return await _dbContext.Colegiaturas
                .Include(c => c.Estudiante)
                .ToListAsync();
        }
    }
}
