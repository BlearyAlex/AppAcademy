using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Features.Productos.Queries.GetProductsByName;
using AppAcademy.Application.Features.Productos.Queries.GetProductsMostSale;
using AppAcademy.Domain.PuntoDeVenta;
using AppAcademy.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AppAcademy.Infrastucture.Repositories
{
    public class ProductoRepository : AsyncRepository<Producto>, IProductoRepository
    {
        public ProductoRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Producto>> GetProductsByCategoria(string categoria)
        {
            var products = await _dbContext.Productos.Where(p => p.Categoria.Nombre == categoria)
                                                     .ToListAsync();
            return products;
        }

        public async Task<List<Producto>> GetProductsByName(string producto)
        {
            var products = await _dbContext.Productos.Where(p => p.Nombre.ToLower().Contains(producto.ToLower()))
                                                     .ToListAsync();
            return products;
        }

        public async Task<List<GetProductsMostSaleVm>> GetProductsMostSale(CancellationToken cancellationToken)
        {
            var products = await _dbContext.DetalleVentas
                .GroupBy(d => d.ProductoId)
                .Select(g => new GetProductsMostSaleVm
                {
                    ProductoId = g.Key,
                    ProductoNombre = g.FirstOrDefault().Producto.Nombre,
                    TotalVendido = g.Sum(d => d.Cantidad)
                })
                .OrderByDescending(p => p.TotalVendido)
                .Take(10)
                .ToListAsync();

            return products;
        }
    }
}
