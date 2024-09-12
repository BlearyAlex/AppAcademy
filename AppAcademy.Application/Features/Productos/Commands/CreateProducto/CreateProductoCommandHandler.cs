using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Productos.Commands.CreateProducto
{
    public class CreateProductoCommandHandler : IRequestHandler<CreateProductoCommand, string>
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProductoCommandHandler> _logger;

        public CreateProductoCommandHandler(IProductoRepository productoRepository, IMapper mapper, ILogger<CreateProductoCommandHandler> logger)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> Handle(CreateProductoCommand request, CancellationToken cancellationToken)
        {
            var prodcutoEntity = _mapper.Map<Producto>(request);
            var newProdcuto = await _productoRepository.AddAsync(prodcutoEntity);

            _logger.LogInformation($"Producto {newProdcuto.ProductoId} fue creado exitosamente");

            return newProdcuto.ProductoId;
        }
    }
}
