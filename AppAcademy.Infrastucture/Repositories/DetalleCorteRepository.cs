using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
using AppAcademy.Infrastucture.Persistence;


namespace AppAcademy.Infrastucture.Repositories
{
    public class DetalleCorteRepository : AsyncRepository<DetalleCorte>, IDetalleCorteRepository
    {
        public DetalleCorteRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
