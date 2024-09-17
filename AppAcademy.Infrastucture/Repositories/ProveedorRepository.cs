using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
using AppAcademy.Infrastucture.Persistence;

namespace AppAcademy.Infrastucture.Repositories
{
    public class ProveedorRepository : AsyncRepository<Proveedor>, IProveedorRepository
    {
        public ProveedorRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
