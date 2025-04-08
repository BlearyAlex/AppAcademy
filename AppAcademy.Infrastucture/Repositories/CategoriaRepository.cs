using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.ViewModel;
using AppAcademy.Domain.Enum;
using AppAcademy.Domain.PuntoDeVenta;
using AppAcademy.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Infrastucture.Repositories
{
    public class CategoriaRepository : AsyncRepository<Categoria>, ICategoriaRepository
    {
        private readonly ILogger<CategoriaRepository> _logger;

        public CategoriaRepository(AppAcademyDbContext dbContext, ILogger<CategoriaRepository> logger) : base(dbContext)
        {
            _logger = logger;
        }

        public async Task<bool> CategoriaTieneProductosActivos(string categoriaId)
        {
            return await _dbContext.Categorias.AnyAsync(c => c.CategoriaId == categoriaId);
        }

        public async Task<List<SalesEvolutionByCategory>> GetSalesEvolutionByCategory(DateTime startDate, DateTime endDate)
        {
            var query = _dbContext.VentaDetalle
                   .Where(vd => vd.Venta!.Fecha >= startDate && vd.Venta.Fecha <= endDate)
                   .Where(vd => vd.Venta.EstadoVenta != VentaEstado.Cancelado)
                   .Include(vd => vd.Producto!)
                        .ThenInclude(p => p.Categoria!);

            var detalles = await query.ToListAsync();
            _logger.LogInformation("Detalles filtrados: {Count}", detalles.Count);

            if (!detalles.Any())
                return new List<SalesEvolutionByCategory>();

            var result = detalles
                   .GroupBy(vd => vd.Producto!.Categoria!.Nombre)
                   .Select(g => new SalesEvolutionByCategory
                   {
                       Categoria = g.Key,
                       Total = g.Sum(vd => vd.Total)
                   })
                   .ToList();

            return result;
        }
    }
}
