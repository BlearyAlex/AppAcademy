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
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateEntradaCommandHandler> _logger;

        public UpdateEntradaCommandHandler(IEntradaRepository entradaRepository, IMapper mapper, ILogger<UpdateEntradaCommandHandler> logger)
        {
            _entradaRepository = entradaRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(UpdateEntradaCommand request, CancellationToken cancellationToken)
        {
           var entradaUpdate = await _entradaRepository.GetByIdWithProductsAsync(request.EntradaId);
            if (entradaUpdate == null)
            {
                throw new NotFoundException(nameof(Entrada), request.EntradaId);
            }

            // Actualizar los datos de la entrada
            entradaUpdate.TotalProductosEntrada = request.TotalProductosEntrada;
            entradaUpdate.FechaDeEntrega = request.FechaDeEntrega;
            entradaUpdate.NumeroFactura = request.NumeroFactura;
            entradaUpdate.VencimientoPago = request.VencimientoPago;
            entradaUpdate.Folio = request.Folio;
            entradaUpdate.Bruto = request.Bruto;

            // Actualizar los productos asociados
            // Primero limpiamos la lista actual de productos
            entradaUpdate.EntradaProductos.Clear();

           // Iterar sobre los productos antes de la actualizacion
           foreach(var productoVm in request.Productos)
            {
                var productoExistente = entradaUpdate.EntradaProductos
                    .FirstOrDefault(p => p.EntradaProductoId == productoVm.EntradaProductoId);

                if(productoExistente != null)
                {
                    // Si el producto ya existe, actualizamos solo los campos necesarios
                    productoExistente.Cantidad = productoVm.Cantidad;
                    productoExistente.Costo = productoVm.Costo;
                    productoExistente.ProductoId = productoVm.ProductoId;
                }
                else
                {
                    // Si el producto no existe, lo agregamos como un nuevo producto
                    entradaUpdate.EntradaProductos.Add(new EntradaProducto
                    {
                        Cantidad = productoVm.Cantidad,
                        Costo = productoVm.Costo,
                        ProductoId = productoVm.ProductoId,
                        EntradaId = entradaUpdate.EntradaId
                    });
                }
            }

            // Guardar los cambios en el repositorio
            await _entradaRepository.UpdateAsync(entradaUpdate);
        }
    }
}
