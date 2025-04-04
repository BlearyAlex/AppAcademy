using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
using AppAcademy.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AppAcademy.Infrastucture.Repositories
{
    public class CategoriaRepository : AsyncRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> CategoriaTieneProductosActivos(string categoriaId)
        {
            return await _dbContext.Categorias.AnyAsync(c => c.CategoriaId == categoriaId);
        }
    }
}
