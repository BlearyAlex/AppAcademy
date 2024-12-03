using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Features.Entradas.Queries.GetEntradasForMonth;
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

            if (entrada == null)
            {
                throw new Exception("Entrada con ID {entradaId} no encontrada");
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

        public async Task<Entrada> GetEntradaByIdWithProductsAsync(string entradaId)
        {
            return await _dbContext.Entradas
                .Include(e => e.EntradaProductos)
                .ThenInclude(ep => ep.Producto)
                .FirstOrDefaultAsync(e => e.EntradaId == entradaId);
        }

        public async Task DeleteProductoAsync(EntradaProducto producto)
        {
            _dbContext.EntradaProductos.Remove(producto);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<GetEntradasForMonthVm> GetEntradasForMonth()
        {
            var month = DateTime.Now.Month;
            var year = DateTime.Now.Year;

            var entradasForMonth = await _dbContext.Entradas
                .Where(e => e.FechaDeEmision.Month == month && e.FechaDeEmision.Year == year)
                .SelectMany(e => e.EntradaProductos, (entrada, EntradaProducto) => EntradaProducto.Costo * EntradaProducto.Cantidad)
                .SumAsync();

            return new GetEntradasForMonthVm
            {
                Mes = month,
                Año = year,
                TotalCompras = entradasForMonth
            };
        }
    }
}