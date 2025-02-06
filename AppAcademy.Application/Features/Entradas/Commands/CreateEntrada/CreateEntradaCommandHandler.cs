    using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Entradas.Commands.CreateEntrada
{
    public class CreateEntradaCommandHandler : IRequestHandler<CreateEntradaCommand, string>
    {
        private readonly IEntradaRepository _entradaRepository;
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateEntradaCommandHandler> _logger;

        public CreateEntradaCommandHandler(
            IEntradaRepository entradaRepository,
            IProductoRepository productoRepository,
            IMapper mapper, 
            ILogger<CreateEntradaCommandHandler> logger)
        {
            _entradaRepository = entradaRepository;
            _productoRepository = productoRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> Handle(CreateEntradaCommand request, CancellationToken cancellationToken)
        {
            var nuevaEntrada = new Entrada
            {
                TotalProductosEntrada = request.TotalProductosEntrada,
                FechaDeEmision = DateTime.Now,
                NumeroFactura = request.NumeroFactura,
                Folio = request.Folio,
                Bruto = request.Bruto
            };

            if(request.Productos != null && request.Productos.Count > 0)
            {
                foreach(var producto in request.Productos)
                {
                    var nuevaEntradaProducto = new EntradaProducto
                    {
                        Cantidad = producto.Cantidad,
                        Costo = producto.Costo,
                        ProductoId = producto.ProductoId
                    };

                    nuevaEntrada.EntradaProductos.Add(nuevaEntradaProducto);

                    // Actualizar al stock de producto
                    var productoEntidad = await _productoRepository.GetById(producto.ProductoId);

                    if(productoEntidad != null)
                    {
                        productoEntidad.Stock += producto.Cantidad;

                        await _productoRepository.UpdateAsync(productoEntidad);
                    }
                }
            }

            var entradaId = await _entradaRepository.CreateEntradaWithProduct(nuevaEntrada);

            return entradaId;
        }
    }
}
