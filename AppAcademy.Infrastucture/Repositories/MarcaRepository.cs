using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.DTOs.Venta;
using AppAcademy.Application.ViewModel.Categoria;
using AppAcademy.Application.ViewModel.Marca;
using AppAcademy.Domain.Enum;
using AppAcademy.Domain.PuntoDeVenta;
using AppAcademy.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Infrastucture.Repositories
{
    public class MarcaRepository : AsyncRepository<Marca>, IMarcaRepository
    {
        private readonly ILogger<MarcaRepository> _logger;

        public MarcaRepository(AppAcademyDbContext dbContext, ILogger<MarcaRepository> logger) : base(dbContext)
        {
            _logger = logger;
        }

        public async Task<bool> MarcaTieneProductosActivos(string marcaId)
        {
            return await _dbContext.Marca.AnyAsync(m => m.MarcaId == marcaId);
        }

        public async Task<List<SalesEvolutionByMarca>> GetSalesEvolutionByMarca(DateTime startDate, DateTime endDate)
        {
            var query = _dbContext.VentaDetalle
                 .Where(vd => vd.Venta!.Fecha >= startDate && vd.Venta.Fecha <= endDate)
                 .Where(vd => vd.Venta.EstadoVenta != VentaEstado.Cancelado)
                 .Include(vd => vd.Producto!)
                    .ThenInclude(p => p.Marca!);

            var detalles = await _dbContext.VentaDetalle
                    .Where(vd => vd.Venta != null &&
                                 vd.Producto != null &&
                                 vd.Producto.Marca != null &&
                                 vd.Venta.Fecha >= startDate &&
                                 vd.Venta.Fecha <= endDate &&
                                 vd.Venta.EstadoVenta != VentaEstado.Cancelado)
                    .Include(vd => vd.Venta)
                    .Include(vd => vd.Producto!)
                        .ThenInclude(p => p.Marca!)
                    .ToListAsync();

            _logger.LogInformation("Detalles filtrados: {Count}", detalles.Count);

            if (!detalles.Any())
                return new List<SalesEvolutionByMarca>();

            var result = detalles
                    .GroupBy(vd => new
                    {
                        Fecha = vd.Venta!.Fecha.Date,
                        Marca = vd.Producto!.Marca!.Nombre ?? "Sin Marca"
                    })
                    .Select(g => new SalesEvolutionByMarca
                    {
                        Fecha = g.Key.Fecha,
                        Marca = g.Key.Marca,
                        Total = g.Sum(vd => vd.Total),
                        Color = g.First().Producto.Marca.Color
                    })
                    .OrderBy(r => r.Fecha)
                    .ToList();

            return result;
        }

        public async Task<List<HighlightedMarca>> GetHighlightedMarcas(DateTime startDate, DateTime endDate)
        {
            var query = _dbContext.VentaDetalle
                .Where(vd => vd.Venta!.Fecha >= startDate && vd.Venta.Fecha <= endDate)
                .Where(vd => vd.Venta.EstadoVenta != VentaEstado.Cancelado)
                .Include(vd => vd.Producto!)
                    .ThenInclude(p => p.Marca!);

            var detalles = await query.ToListAsync();
            _logger.LogInformation("Detalles filtrados: {Count}", detalles.Count);

            if (!detalles.Any())
                return new List<HighlightedMarca>();

            var result = detalles
                .GroupBy(vd => vd.Producto!.Marca!.Nombre)
                .Select(g => new HighlightedMarca
                {
                    Marca = g.Key,
                    Total = g.Sum(vd => vd.Total),
                    Color = g.First().Producto.Marca.Color
                })
                .OrderByDescending(c => c.Total)
                .ToList();

            return result;
        }

        public async Task<List<SalesByMarca>> GetSalesByMarca(DateTime startDate, DateTime endDate)
        {
            var query = _dbContext.VentaDetalle
                  .Where(vd => vd.Venta!.Fecha >= startDate && vd.Venta.Fecha <= endDate)
                  .Where(vd => vd.Venta.EstadoVenta != VentaEstado.Cancelado)
                  .Include(vd => vd.Producto!)
                      .ThenInclude(p => p.Marca!);

            var detalles = await query.ToListAsync();
            _logger.LogInformation("Detalles filtrados: {Count}", detalles.Count);

            if (!detalles.Any())
                return new List<SalesByMarca> { };

            var result = detalles
                .GroupBy(vd => vd.Producto!.Marca!.Nombre)
                .Select(g => new SalesByMarca
                {
                    Marca = g.Key,
                    Total = g.Sum(vd => vd.Total),
                    Color = g.First().Producto.Marca.Color
                })
                .ToList();

            return result;
        }
    }
}
