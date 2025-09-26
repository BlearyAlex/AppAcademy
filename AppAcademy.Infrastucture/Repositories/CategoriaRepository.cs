using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.DTOs.Venta;
using AppAcademy.Application.ViewModel.Categoria;
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
            return await _dbContext.Productos.AnyAsync(c => c.CategoriaId == categoriaId);
        }

        public async Task<List<SalesEvolutionByCategory>> GetSalesEvolutionByCategory(DateTime startDate, DateTime endDate)
        {
            var query = _dbContext.VentaDetalle
                   .Where(vd => vd.Venta!.Fecha >= startDate && vd.Venta.Fecha <= endDate)
                   .Where(vd => vd.Venta.EstadoVenta != VentaEstado.Cancelado)
                   .Include(vd => vd.Producto!)
                        .ThenInclude(p => p.Categoria!);

            var detalles = await _dbContext.VentaDetalle
                    .Where(vd => vd.Venta != null &&
                                 vd.Producto != null &&
                                 vd.Producto.Categoria != null &&
                                 vd.Venta.Fecha >= startDate &&
                                 vd.Venta.Fecha <= endDate &&
                                 vd.Venta.EstadoVenta != VentaEstado.Cancelado)
                    .Include(vd => vd.Venta)
                    .Include(vd => vd.Producto!)
                        .ThenInclude(p => p.Categoria!)
                    .ToListAsync();

            _logger.LogInformation("Detalles filtrados: {Count}", detalles.Count);

            if (!detalles.Any())
                return new List<SalesEvolutionByCategory>();

            var result = detalles
                    .GroupBy(vd => new
                    {
                        Fecha = vd.Venta!.Fecha.Date,
                        Categoria = vd.Producto!.Categoria!.Nombre ?? "Sin Categoria"
                    })
                    .Select(g => new SalesEvolutionByCategory
                    {
                        Fecha = g.Key.Fecha,
                        Categoria = g.Key.Categoria,
                        Total = g.Sum(vd => vd.Total),
                        Color = g.First().Producto.Categoria.Color
                    })
                    .OrderBy(r => r.Fecha)
                    .ToList();

            return result;
        }

        public async Task<List<HighlightedCategory>> GetHighlightedCategories(DateTime startDate, DateTime endDate)
        {
            var query = _dbContext.VentaDetalle
                .Where(vd => vd.Venta!.Fecha >= startDate && vd.Venta.Fecha <= endDate)
                .Where(vd => vd.Venta.EstadoVenta != VentaEstado.Cancelado)
                .Include(vd => vd.Producto!)
                    .ThenInclude(p => p.Categoria!);

            var detalles = await query.ToListAsync();
            _logger.LogInformation("Detalles filtrados: {Count}", detalles.Count);

            if (!detalles.Any())
                return new List<HighlightedCategory>();

            var result = detalles
                .GroupBy(vd => vd.Producto!.Categoria!.Nombre)
                .Select(g => new HighlightedCategory
                {
                    Categoria = g.Key,
                    Total = g.Sum(vd => vd.Total),
                    Color = g.First().Producto.Categoria.Color
                })
                .OrderByDescending(c => c.Total)
                .ToList();

            return result;
        }

        public async Task<List<SalesByCategory>> GetSalesByCategory(DateTime startDate, DateTime endDate)
        {
            var query = _dbContext.VentaDetalle
                  .Where(vd => vd.Venta!.Fecha >= startDate && vd.Venta.Fecha <= endDate)
                  .Where(vd => vd.Venta.EstadoVenta != VentaEstado.Cancelado)
                  .Include(vd => vd.Producto!)
                      .ThenInclude(p => p.Categoria!);

            var detalles = await query.ToListAsync();
            _logger.LogInformation("Detalles filtrados: {Count}", detalles.Count);

            if (!detalles.Any())
                return new List<SalesByCategory> { };

            var result = detalles
                .GroupBy(vd => vd.Producto!.Categoria!.Nombre)
                .Select(g => new SalesByCategory
                {
                    Categoria = g.Key,
                    Total = g.Sum(vd => vd.Total),
                    Color = g.First().Producto.Categoria.Color
                })
                .ToList();

            return result;
        }
    }
}
