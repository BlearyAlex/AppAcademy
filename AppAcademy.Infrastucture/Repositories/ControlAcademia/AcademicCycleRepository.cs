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
                   CicloAcademico = a.CicloAcademico,
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
                        CicloAcademico = a.CicloAcademico,
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

        public async Task<List<(int mes, int anio)>> GetMonthsAvailable(int studentId, AcademicCycle cycle)
        {
            // Generar la lista de meses en el ciclo académico
            var mesesDelCiclo = new List<(int mes, int anio)>();
            DateTime fechaActual = cycle.FechaInicio;
            while (fechaActual <= cycle.FechaFin)
            {
                mesesDelCiclo.Add((fechaActual.Month, fechaActual.Year));
                fechaActual = fechaActual.AddMonths(1);
            }

            // Obtener los pagos registrados del estudiante en el ciclo académico
            var pagosAnon = await _dbContext.Payments
                .Where(p => p.StudentId == studentId &&
                            p.FechaPago >= cycle.FechaInicio &&
                            p.FechaPago <= cycle.FechaFin)
                .Select(p => new { Mes = (int)p.MesPagado, p.AnioPagado })
                .ToListAsync();

            // Convertir a una lista de tuplas (int, int)
            var pagosRegistradosTuples = pagosAnon.Select(p => (p.Mes, p.AnioPagado)).ToList();

            // Crear un HashSet para hacer búsquedas eficientes
            var pagosRegistradosSet = new HashSet<(int, int)>(pagosRegistradosTuples);

            // Filtrar los meses disponibles (que aún no se han pagado)
            var mesesDisponibles = mesesDelCiclo
                .Where(m => !pagosRegistradosSet.Contains(m))
                .ToList();

            return mesesDisponibles;
        }

        public async Task<bool> AcademicCycleTieneEstudiantes(int cycleId)
        {
            return await _dbContext.Students.AnyAsync(s => s.AcademicCycleId == cycleId);
        }
    }
}
