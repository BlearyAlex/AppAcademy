using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Features.Productos.Queries.GetProductsByName;
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
            var products = await _dbContext.Productos.Where(p => p.Nombre ==  producto)
                                                     .ToListAsync();
            return products;
        }
    }
}
