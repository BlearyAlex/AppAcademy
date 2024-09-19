using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
using AppAcademy.Infrastucture.Persistence;

namespace AppAcademy.Infrastucture.Repositories
{
    public class DetallePagoRepository : AsyncRepository<DetallePago>, IDetallePagoRepository
    {
        public DetallePagoRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
