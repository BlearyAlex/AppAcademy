using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
using AppAcademy.Infrastucture.Persistence;

namespace AppAcademy.Infrastucture.Repositories
{
    public class InventarioRepository : AsyncRepository<Inventario>, IInventarioRepository
    {
        public InventarioRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
