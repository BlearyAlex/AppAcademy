using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
using AppAcademy.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AppAcademy.Infrastucture.Repositories
{
    public class EntradaRepository : AsyncRepository<Entrada>, IEntradaRepository
    {
        public EntradaRepository(AppAcademyDbContext dbContext) : base(dbContext)
        {
        }

        public async Task CreateEntradaWithProduct(Entrada nuevaEntrada, List<EntradaProducto> productos)
        {
            using var transiction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                _dbContext.Entradas.Add(nuevaEntrada);
                await _dbContext.SaveChangesAsync();


                foreach (var producto in productos)
                {
                    producto.EntradaProductoId = nuevaEntrada.EntradaId;
                    _dbContext.EntradaProductos.Add(producto);
                }

                await _dbContext.SaveChangesAsync();
                await transiction.CommitAsync();
            }
            catch (Exception)
            {
                await transiction.RollbackAsync();
                throw;
            }

        }

        public async Task DeleteEntrada(string entradaId)
        {
            var entrada = await _dbContext.Entradas
                .FirstOrDefaultAsync(e => e.EntradaId == entradaId);

            if(entrada == null)
            {
                throw new Exception("Entrdada no encontrada");
            }

            _dbContext.Entradas.Remove(entrada);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<List<Entrada>> GetEntradasWithProductos()
        {
            return await _dbContext.Entradas
                .Include(e => e.EntradaProductos)
                .ToListAsync();
        }

        public async Task<Entrada> ObtenerEntradaPorId(string entradaId)
        {
            return await _dbContext.Entradas
                .Include(e => e.EntradaProductos)
                .FirstOrDefaultAsync(e => e.EntradaId == entradaId);
        }

        public async Task UpdateEntradaWithProducts(Entrada entradaEditada, List<EntradaProducto> productosActualizados)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                // Actualizar la entrada
                _dbContext.Entradas.Update(entradaEditada);
                await _dbContext.SaveChangesAsync();

                // Obtener los productos existentes
                var productosExistentes = await _dbContext.EntradaProductos
                    .Where(p => p.EntradaId == entradaEditada.EntradaId).ToListAsync();

                // Eliminar productos no presentes en los actualizados
                var productosAEliminar = productosExistentes
                    .Where(p => !productosActualizados.Any(pa => pa.ProductoId == p.ProductoId)).ToList();
                _dbContext.EntradaProductos.RemoveRange(productosAEliminar);

                // Insertar o actualizar los productos nuevos
                foreach(var productoActualizado in productosActualizados)
                {
                    var productoExistente = productosExistentes
                        .FirstOrDefault(p => p.ProductoId == productoActualizado.ProductoId);

                    if(productoExistente != null)
                    {
                        // Actualizar producto
                        productoExistente.Cantidad = productoActualizado.Cantidad;
                        productoExistente.Costo = productoActualizado.Costo;
                    }
                    else
                    {
                        // Insertar nuevo producto
                        productoActualizado.EntradaId = entradaEditada.EntradaId;
                        _dbContext.EntradaProductos.Add(productoActualizado);
                    }
                }

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
