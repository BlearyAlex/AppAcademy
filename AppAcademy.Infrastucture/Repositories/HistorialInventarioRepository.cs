using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
using AppAcademy.Infrastucture.Persistence;

namespace AppAcademy.Infrastucture.Repositories
{
    public class HistorialInventarioRepository : AsyncRepository<HistorialInventario>, IHistorialInventarioRepository
    {
        public HistorialInventarioRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
