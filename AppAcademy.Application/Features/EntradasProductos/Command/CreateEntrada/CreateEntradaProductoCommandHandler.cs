using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.EntradasProductos.Command.CreateEntrada
{
    public class CreateEntradaProductoCommandHandler : IRequestHandler<CreateEntradaProductoCommand, string>
    {
        private readonly IEntradaProductoRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateEntradaProductoCommandHandler> _logger;

        public CreateEntradaProductoCommandHandler(IEntradaProductoRepository repository, IMapper mapper, ILogger<CreateEntradaProductoCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> Handle(CreateEntradaProductoCommand request, CancellationToken cancellationToken)
        {
            var entradaProducto = _mapper.Map<EntradaProducto>(request);
            var newEntradaProducto = await _repository.AddAsync(entradaProducto);

            return newEntradaProducto.EntradaProductoId;
        }
    }
}
