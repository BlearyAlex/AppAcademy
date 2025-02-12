using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Features.Productos.Queries.GetAllProductos;
using AppAcademy.Application.Features.Productos.Queries.GetProductById;
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

        public async Task<List<GetAllProductosVm>> GetAllProductos()
        {
            var productos = await _dbContext.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Marca)
                .Include(p => p.Proveedor)
                .Select(p => new GetAllProductosVm
                {
                    ProductoId = p.ProductoId,
                    Nombre = p.Nombre,
                    CodigoBarras = p.CodigoBarras,
                    Descripcion = p.Descripcion,
                    Imagen = p.Imagen,
                    Costo = p.Costo,
                    Utilidad = p.Utilidad,
                    Precio = p.Precio,
                    Color = p.Color,
                    EstadoProducto = p.EstadoProducto.ToString(),
                    Stock = p.Stock,
                    Categoria = new GetAllCategoriaVm
                    {
                        CategoriaId = p.Categoria.CategoriaId,
                        Nombre = p.Categoria.Nombre,
                        Color = p.Categoria.Color,
                    },
                    Marca = new GetAllMarcaVm
                    {
                        MarcaId = p.Marca.MarcaId,
                        Nombre = p.Marca.Nombre,
                        Color = p.Marca.Color,
                    },
                    Proveedor = new GetAllProveedorVm
                    {
                        ProveedorId = p.Proveedor.ProveedorId,
                        Nombre = p.Proveedor.Nombre,
                        Color = p.Proveedor.Color
                    }
                }).ToListAsync();

            return productos;
        }

        public async Task<GetProductByIdVm> GetProductById(string productoId)
        {
            var product = await _dbContext.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Marca)
                .Include(p => p.Proveedor)
                .Where(p => p.ProductoId ==  productoId)
                .Select(p => new GetProductByIdVm
                {
                    ProductoId = p.ProductoId,
                    Nombre = p.Nombre,
                    CodigoBarras = p.CodigoBarras,
                    Descripcion = p.Descripcion,
                    Imagen = p.Imagen,
                    Costo = p.Costo,
                    Utilidad = p.Utilidad,
                    Precio = p.Precio,
                    EstadoProducto = p.EstadoProducto,
                    Stock = p.Stock,
                    Color = p.Color,
                    Categoria = new GetByIdCategoriaVm
                    {
                        CategoriaId = p.Categoria.CategoriaId,
                        Nombre = p.Categoria.Nombre,
                        Color = p.Categoria.Color
                    },
                    Marca = new GetByIdMarcaVm
                    {
                        MarcaId = p.Marca.MarcaId,
                        Nombre = p.Marca.Nombre,
                        Color = p.Marca.Color
                    },
                    Proveedor = new GetByIdProveedorVm
                    {
                        ProveedorId = p.Proveedor.ProveedorId,
                        Nombre = p.Proveedor.Nombre,
                        Color = p.Proveedor.Color
                    }
                }).FirstOrDefaultAsync();

            return product;
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
