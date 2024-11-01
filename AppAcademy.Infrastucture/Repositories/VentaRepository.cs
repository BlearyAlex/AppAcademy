using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
using AppAcademy.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;

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
    }
}
