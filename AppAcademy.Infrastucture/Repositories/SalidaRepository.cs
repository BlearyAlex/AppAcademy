using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
using AppAcademy.Infrastucture.Persistence;

namespace AppAcademy.Infrastucture.Repositories
{
    public class SalidaRepository : AsyncRepository<Salida>, ISalidaRepository
    {
        public SalidaRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
