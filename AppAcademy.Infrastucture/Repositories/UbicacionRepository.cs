using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
using AppAcademy.Infrastucture.Persistence;

namespace AppAcademy.Infrastucture.Repositories
{
    public class UbicacionRepository : AsyncRepository<Ubicacion>, IUbicacionRepository
    {
        public UbicacionRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
