using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
using AppAcademy.Infrastucture.Persistence;

namespace AppAcademy.Infrastucture.Repositories
{
    public class CorteRepository : AsyncRepository<Corte>, ICorteRepository
    {
        public CorteRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
