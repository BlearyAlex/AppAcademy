using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Features.Productos.Queries.GetAllProductos;
using AppAcademy.Application.Features.Productos.Queries.GetProductById;
using AppAcademy.Application.ViewModel.Producto;
using AppAcademy.Domain.Enum;
using AppAcademy.Domain.PuntoDeVenta;
using AppAcademy.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Infrastucture.Repositories
{
    public class ProductoRepository : AsyncRepository<Producto>, IProductoRepository
    {
        private readonly ILogger<ProductoRepository> _logger;

        public ProductoRepository(AppAcademyDbContext dbContext, ILogger<ProductoRepository> logger) : base(dbContext)
        {
            _logger = logger;
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
                    FechaRegistro = p.FechaRegistro,
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
                    FechaRegistro = p.FechaRegistro,
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

        public async Task<bool> ProductoTieneVentasActivas(string productoId)
        {
            return await _dbContext.VentaDetalle.AnyAsync(vd => vd.ProductoId == productoId);
        }

        #region DirectMethods
        public async Task<List<SalesEvolutionByProducto>> GetSalesEvolutionByProducto(DateTime startDate, DateTime endDate)
        {
            var query = _dbContext.VentaDetalle
                   .Where(vd => vd.Venta!.Fecha >= startDate && vd.Venta.Fecha <= endDate)
                   .Where(vd => vd.Venta.EstadoVenta != VentaEstado.Cancelado)
                   .Include(vd => vd.Producto!);

            var detalles = await _dbContext.VentaDetalle
                    .Where(vd => vd.Venta != null &&
                                 vd.Producto != null &&
                                 vd.Venta.Fecha >= startDate &&
                                 vd.Venta.Fecha <= endDate &&
                                 vd.Venta.EstadoVenta != VentaEstado.Cancelado)
                    .Include(vd => vd.Venta)
                    .Include(vd => vd.Producto!)
                    .ToListAsync();

            _logger.LogInformation("Detalles filtrados: {Count}", detalles.Count);

            if (!detalles.Any())
                return new List<SalesEvolutionByProducto>();

            var result = detalles
                    .GroupBy(vd => new
                    {
                        Fecha = vd.Venta!.Fecha.Date,
                        Producto = vd.Producto!.Nombre ?? "Producto Sin Nombre"
                    })
                    .Select(g => new SalesEvolutionByProducto
                    {
                        Fecha = g.Key.Fecha,
                        Producto = g.Key.Producto,
                        Total = g.Sum(vd => vd.Total),
                        Color = g.First().Producto.Color
                    })
                    .OrderBy(r => r.Fecha)
                    .ToList();

            return result;
        }

        public async Task<List<HighlightedProducto>> GetHighlightedProducto(DateTime startDate, DateTime endDate)
        {
            var query = _dbContext.VentaDetalle
                .Where(vd => vd.Venta!.Fecha >= startDate && vd.Venta.Fecha <= endDate)
                .Where(vd => vd.Venta.EstadoVenta != VentaEstado.Cancelado)
                .Include(vd => vd.Producto!);

            var detalles = await query.ToListAsync();
            _logger.LogInformation("Detalles filtrados: {Count}", detalles.Count);

            if (!detalles.Any())
                return new List<HighlightedProducto>();

            var result = detalles
                .GroupBy(vd => vd.Producto!.Nombre)
                .Select(g => new HighlightedProducto
                {
                    Producto = g.Key,
                    Total = g.Sum(vd => vd.Total),
                    Color = g.First().Producto.Color
                })
                .OrderByDescending(c => c.Total)
                .ToList();

            return result;
        }

        public async Task<List<SalesByProducto>> GetSalesByProducto(DateTime startDate, DateTime endDate)
        {
            var query = _dbContext.VentaDetalle
                  .Where(vd => vd.Venta!.Fecha >= startDate && vd.Venta.Fecha <= endDate)
                  .Where(vd => vd.Venta.EstadoVenta != VentaEstado.Cancelado)
                  .Include(vd => vd.Producto!);

            var detalles = await query.ToListAsync();
            _logger.LogInformation("Detalles filtrados: {Count}", detalles.Count);

            if (!detalles.Any())
                return new List<SalesByProducto> { };

            var result = detalles
                .GroupBy(vd => vd.Producto!.Nombre)
                .Select(g => new SalesByProducto
                {
                    Producto= g.Key,
                    Total = g.Sum(vd => vd.Total),
                    Color = g.First().Producto.Color
                })
                .ToList();

            return result;
        }
        #endregion

        #region Metodo auxiliares
        public async Task<bool> DescontarStock(string productoId, int cantidad)
        {
            var product = await _dbContext.Productos.FirstOrDefaultAsync(p => p.ProductoId == productoId);

            if (product == null)
                throw new Exception($"Producto con ID {productoId} no encontrado.");

            if (product.Stock < cantidad)
                throw new Exception($"Stock insuficiente para el producto {product.Nombre}. Disponible: {product.Stock}, requerido: {cantidad}");

            product.Stock -= cantidad;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AgregarStock(string productoId, int cantidad)
        {
            try
            {
                var producto = await _dbContext.Productos.FirstOrDefaultAsync(p => p.ProductoId == productoId);
                if (producto != null)
                {
                    producto.Stock += cantidad;  // Aumenta el stock
                    await _dbContext.SaveChangesAsync();  // Guarda los cambios
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public async Task<bool> CleanImageProductAsync(string imageName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(imageName)) return false;

                var producto = await _dbContext.Productos
                    .FirstOrDefaultAsync(p => p.Imagen != null && p.Imagen.EndsWith(imageName));

                if (producto == null)
                    return false;

                producto.Imagen = null;
                _dbContext.Productos.Update(producto);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al limpiar el registro de la tabla");
                throw;
            }
        }
        #endregion
    }
}
