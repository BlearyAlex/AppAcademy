using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Application.Features.AcademicCycles.Queries.GetAllCycles;
using AppAcademy.Application.Features.AcademicCycles.Queries.GetCycle;
using AppAcademy.Domain.ControlAcademia;
using AppAcademy.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AppAcademy.Infrastucture.Repositories.ControlAcademia
{
    public class AcademicCycleRepository : AsyncRepository<AcademicCycle>, IAcademicCycleRepository
    {
        public AcademicCycleRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<GetAllCyclesVm>> GetAllCyclesWithCareer()
        {
            try
            {
                var cycles = await _dbContext.AcademicCycles
               .Include(a => a.Career)
               .Select(a => new GetAllCyclesVm
               {
                   AcademicCycleId = a.AcademicCycleId,
                   NumeroCiclo = a.NumeroCiclo,
                   Color = a.Color,
                   FechaInicio = a.FechaInicio,
                   FechaFin = a.FechaFin,
                   Career = new GetCareerCycleVm
                   {
                       CareerId = a.Career.CareerId,
                       Nombre = a.Career.Nombre,
                       Color = a.Color
                   }
               }).ToListAsync();

                return cycles;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<GetCycleVm> GetCycleWithCareer(int cycleId)
        {
            try
            {
                var cycle = await _dbContext.AcademicCycles
                    .Include(a => a.Career)
                    .Where(a => a.AcademicCycleId == cycleId)
                    .Select(a => new GetCycleVm
                    {
                        AcademicCycleId = a.AcademicCycleId,
                        NumeroCiclo = a.NumeroCiclo,
                        Color = a.Color,
                        FechaInicio = a.FechaInicio,
                        FechaFin = a.FechaFin,
                        Career = new GetCareerCycleVm
                        {
                            CareerId = a.Career.CareerId,
                            Nombre = a.Career.Nombre,
                            Color = a.Color
                        }
                    }).FirstOrDefaultAsync();

                return cycle;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
