using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
using AppAcademy.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AppAcademy.Infrastucture.Repositories
{
    public class MarcaRepository : AsyncRepository<Marca>, IMarcaRepository
    {
        public MarcaRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> MarcaTieneProductosActivos(string marcaId)
        {
            return await _dbContext.Marca.AnyAsync(m => m.MarcaId == marcaId);
        }
    }
}
