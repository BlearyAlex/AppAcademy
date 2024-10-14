using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
using AppAcademy.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AppAcademy.Infrastucture.Repositories
{
    public class EntradaRepository : AsyncRepository<Entrada>, IEntradaRepository
    {
        public EntradaRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<string> CreateEntradaWithProduct(Entrada nuevaEntrada)
        {
            _dbContext.Entradas.Add(nuevaEntrada);
            await _dbContext.SaveChangesAsync();

            return nuevaEntrada.EntradaId;
        }

        public async Task DeleteEntrada(string entradaId)
        {
            var entrada = await _dbContext.Entradas
                .FirstOrDefaultAsync(e => e.EntradaId == entradaId);

            if(entrada == null)
            {
                throw new Exception("Entrdada no encontrada");
            }

            _dbContext.Entradas.Remove(entrada);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<List<Entrada>> GetEntradasWithProductos()
        {
            return await _dbContext.Entradas
                .Include(e => e.EntradaProductos)
                .ToListAsync();
        }

        public async Task<Entrada> ObtenerEntradaPorId(string entradaId)
        {
            return await _dbContext.Entradas
                .Include(e => e.EntradaProductos)
                .FirstOrDefaultAsync(e => e.EntradaId == entradaId);
        }

        public async Task<Entrada> GetByIdWithProductsAsync(string entradaId)
        {
            return await _dbContext.Entradas
                .Include(e => e.EntradaProductos)
                .FirstOrDefaultAsync(e => e.EntradaId == entradaId);
        }
    }
}
