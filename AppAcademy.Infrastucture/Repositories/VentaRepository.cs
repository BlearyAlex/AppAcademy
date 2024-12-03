using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Features.Ventas.Queries.GetVentaForDay;
using AppAcademy.Application.Features.Ventas.Queries.GetVentaForMonth;
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

        public async Task<List<GetVentasForDateVm>> GetVentasForDate(CancellationToken cancellationToken, string periodo)
        {
            var ventas = await _dbContext.Ventas
                .Where(v => v.FechaCompra != DateTime.MinValue) // Solo verifica si la fecha no es la fecha mínima
                .Select(v => new
                {
                    Fecha = v.FechaCompra,
                    Neto = v.Neto
                })
                .ToListAsync(cancellationToken);

            return periodo.ToLower() switch
            {
                "dia" => ventas.GroupBy(v => v.Fecha.Date)
                               .Select(g => new GetVentasForDateVm
                               {
                                   Fecha = g.Key.ToString("yyyy-MM-dd"),
                                   TotalVentas = g.Sum(v => v.Neto)
                               }).ToList(),

                "semana" => ventas.GroupBy(v => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(v.Fecha, CalendarWeekRule.FirstDay, DayOfWeek.Monday))
                                  .Select(g => new GetVentasForDateVm
                                  {
                                      Fecha = $"Semana {g.Key}",
                                      TotalVentas = g.Sum(v => v.Neto),
                                  }).ToList(),

                "mes" => ventas.GroupBy(v => new { v.Fecha.Year, v.Fecha.Month })
                               .Select(g => new GetVentasForDateVm
                               {
                                   Fecha = $"{g.Key.Month}/{g.Key.Year}",
                                   TotalVentas = g.Sum(v => v.Neto),
                               }).ToList(),

                _ => throw new ArgumentException("El periodo no es válido. Usa 'dia', 'semana' o 'mes'.")
            };
        }


        public async Task<GetVentaForMonthVm> GetVentaForMont()
        {
            var month = DateTime.Now.Month;
            var year = DateTime.Now.Year;

            var ventas = await _dbContext.Ventas
                .Where(v => v.FechaCompra.Month == month && v.FechaCompra.Year == year)
                .SelectMany(e => e.DetalleVentas, (venta, DetalleVenta) => DetalleVenta.Costo * DetalleVenta.Cantidad)
                .SumAsync();

            return new GetVentaForMonthVm
            {
                Mes = month,
                Año = year,
                TotalVentas = ventas
            };
        }

        public async Task<GetVentaForDayVm> GetVentaForDay()
        {
            // Ajusta la hora al huso horario de México
            var mexicoTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");
            var todayStart = TimeZoneInfo.ConvertTime(DateTime.Today, mexicoTimeZone); // 00:00:00 del día actual en hora local
            var tomorrowStart = todayStart.AddDays(1); // 00:00:00 del siguiente día en hora local

            var ventas = await _dbContext.Ventas
                .Where(v => v.FechaCompra >= todayStart && v.FechaCompra < tomorrowStart)
                .SumAsync(v => v.Neto);

            return new GetVentaForDayVm
            {
                Fecha = todayStart.ToString("yyyy-MM-dd"), 
                TotalVenta = ventas
            };
        }


    }
}

