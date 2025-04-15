using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Domain.ControlAcademia;
using AppAcademy.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AppAcademy.Infrastucture.Repositories.ControlAcademia
{
    public class CareerRepository : AsyncRepository<Career>, ICareerRepository
    {
        public CareerRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Career>> GetAllCareerFilter()
        {
            var careers = await _dbContext.Careers
                .Where(c => c.Activa == true)
                .ToListAsync();

            return careers;
        }

        public async Task<bool> CareerTienePagosActivos(int careerId)
        {
            return await _dbContext.Payments.AnyAsync(s => s.CareerId== careerId);
        }
    }
}
