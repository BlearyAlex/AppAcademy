using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
using AppAcademy.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AppAcademy.Infrastucture.Repositories
{
    public class ProveedorRepository : AsyncRepository<Proveedor>, IProveedorRepository
    {
        public ProveedorRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> ProveedorTieneProductosActivos(string proveedorId)
        {
           return await _dbContext.Productos.AnyAsync(p => p.ProveedorId  == proveedorId);
        }
    }
}
