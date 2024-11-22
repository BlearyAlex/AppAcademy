using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Features.Ventas.Queries.GetVentasForDate;
using AppAcademy.Domain.PuntoDeVenta;
using AppAcademy.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace AppAcademy.Infrastucture.Repositories
{
    public class VentaRepository : AsyncRepository<Venta>, IVentaRepository
    {
        public VentaRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<string> CreateVentaWithProduct(Venta nuevaVenta)
        {
            _dbContext.Ventas.Add(nuevaVenta);
            await _dbContext.SaveChangesAsync();

            return nuevaVenta.VentaId;
        }

        public async Task<List<Venta>> GetVentasWithProductos()
        {
            return await _dbContext.Ventas
                .Include(v => v.DetalleVentas)
                .ToListAsync();
        }

        public async Task<Venta> GetVentaByIdWithProductsAsync(string ventaId)
        {
            return await _dbContext.Ventas
                .Include(v => v.DetalleVentas)
                .ThenInclude(dv => dv.Producto)
                .FirstOrDefaultAsync(v => v.VentaId == ventaId);
        }

        public async Task DeleteDetalleVentaAsync(DetalleVenta detalleVenta)
        {
            _dbContext.DetalleVentas.Remove(detalleVenta);
            await _dbContext.SaveChangesAsync();
        }

        public async Task <List<GetVentasForDateVm>> GetVentasForDate(CancellationToken cancellationToken, string periodo)
        {
            var ventas = await _dbContext.Ventas
                .Where(v => v.FechaCompra.HasValue)
                .Select(v => new
                {
                    Fecha = v.FechaCompra.Value,
                    Neto = v.Neto,
                    TotalProductos = v.TotalProductos
                })
                .ToListAsync(cancellationToken);

            return periodo.ToLower() switch
            {
                "dia" => ventas.GroupBy(v => v.Fecha.Date)
                               .Select(g => new GetVentasForDateVm
                               {
                                   Fecha = g.Key.ToString("yyyy-MM-dd"),
                                   TotalVentas = g.Sum(v => v.Neto),
                                   TotalProductos = g.Sum(v => v.TotalProductos)
                               }).ToList(),

                "semana" => ventas.GroupBy(v => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(v.Fecha, CalendarWeekRule.FirstDay, DayOfWeek.Monday))
                                  .Select(g => new GetVentasForDateVm
                                  {
                                      Fecha = $"Semana {g.Key}",
                                      TotalVentas = g.Sum(v => v.Neto),
                                      TotalProductos = g.Sum(v => v.TotalProductos)
                                  }).ToList(),

                "mes" => ventas.GroupBy(v => new { v.Fecha.Year, v.Fecha.Month })
                               .Select(g => new GetVentasForDateVm
                               {
                                   Fecha = $"{g.Key.Month}/{g.Key.Year}",
                                   TotalVentas = g.Sum(v => v.Neto),
                                   TotalProductos = g.Sum(v => v.TotalProductos)
                               }).ToList(),

                _ => throw new ArgumentException("El periodo no es válido. Usa 'dia', 'semana' o 'mes'.")
            };
        }
    }
}

