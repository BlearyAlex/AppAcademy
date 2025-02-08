using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Exceptions;
using AppAcademy.Domain.PuntoDeVenta;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Entradas.Commands.UpdateEntrada
{
    public class UpdateEntradaCommandHandler : IRequestHandler<UpdateEntradaCommand>
    {
        private readonly IEntradaRepository _entradaRepository;
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateEntradaCommandHandler> _logger;

        public UpdateEntradaCommandHandler(IEntradaRepository entradaRepository, IProductoRepository productoRepository, IMapper mapper, ILogger<UpdateEntradaCommandHandler> logger)
        {
            _entradaRepository = entradaRepository;
            _productoRepository = productoRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(UpdateEntradaCommand request, CancellationToken cancellationToken)
        {
            var entradaUpdate = await _entradaRepository.GetEntradaByIdWithProductsAsync(request.EntradaId);
            if (entradaUpdate == null)
            {
                throw new NotFoundException(nameof(Entrada), request.EntradaId);
            }

            // Actualizar los datos de la entrada
            entradaUpdate.TotalProductosEntrada = request.TotalProductosEntrada;
            entradaUpdate.FechaDeEmision = DateTime.Now; // Asegúrate de usar la fecha proporcionada
            entradaUpdate.NumeroFactura = request.NumeroFactura;
            entradaUpdate.Folio = request.Folio;
            entradaUpdate.Bruto = request.Bruto;

            // Actualizar los productos asociados
            // Primero limpiamos la lista actual de productos
            var productosEliminar = entradaUpdate.EntradaProductos.ToList();

            // Iterar sobre los productos antes de la actualizacion
            foreach (var productoVm in request.Productos)
            {
                // Verifica si es un producto existente o nuevo
                if (string.IsNullOrEmpty(productoVm.EntradaProductoId))
                {
                    // Si es nuevo, lo agregamos
                    var nuevaEntradaProducto = new EntradaProducto
                    {
                        Cantidad = productoVm.Cantidad,
                        Costo = productoVm.Costo,
                        ProductoId = productoVm.ProductoId,
                        EntradaId = entradaUpdate.EntradaId
                    };

                    entradaUpdate.EntradaProductos.Add(nuevaEntradaProducto);

                    //Actualizar el stock de producto
                    var productoEntidad = await _productoRepository.GetById(productoVm.ProductoId);
                    if(productoEntidad != null)
                    {
                        productoEntidad.Stock += productoVm.Cantidad;
                        await _productoRepository.UpdateAsync(productoEntidad);
                    }
                }
                else
                {
                    // Si el producto ya existe, actualizamos los campos
                    var productoExistente = entradaUpdate.EntradaProductos
                        .FirstOrDefault(p => p.EntradaProductoId == productoVm.EntradaProductoId);

                    if (productoExistente != null)
                    {
                        // Actualiza solo los campos necesarios
                        var cantidadAnterior = productoExistente.Cantidad;  // Guarda la cantidad anterior para calcular el cambio en el stock
                        productoExistente.Cantidad = productoVm.Cantidad;
                        productoExistente.Costo = productoVm.Costo;

                        // Actualizamos el stock del producto
                        var productoEntidad = await _productoRepository.GetById(productoVm.ProductoId);
                        if (productoEntidad != null)
                        {
                            productoEntidad.Stock += (productoVm.Cantidad - cantidadAnterior); 
                            await _productoRepository.UpdateAsync(productoEntidad);
                        }

                        // Elimina de la lista de productos a eliminar
                        productosEliminar.Remove(productoExistente);
                    }
                }
            }

            // Eliminar los productos que ya no están en la lista
            foreach (var productoParaEliminar in productosEliminar)
            {
                await _entradaRepository.DeleteProductoAsync(productoParaEliminar);
            }

            // Guardar los cambios en el repositorio
            await _entradaRepository.UpdateAsync(entradaUpdate);
        }

    }
}
