using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Exceptions;
using AppAcademy.Domain.PuntoDeVenta;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.EntradasProductos.Command.UpdateEntrada
{
    public class UpdateEntradaProductoCommandHandler : IRequestHandler<UpdateEntradaProductoCommand>
    {
        private readonly IEntradaProductoRepository _entradaProductoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateEntradaProductoCommandHandler> _logger;

        public UpdateEntradaProductoCommandHandler(IEntradaProductoRepository entradaProductoRepository, IMapper mapper, ILogger<UpdateEntradaProductoCommandHandler> logger)
        {
            _entradaProductoRepository = entradaProductoRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(UpdateEntradaProductoCommand request, CancellationToken cancellationToken)
        {
            var findEntrada = await _entradaProductoRepository.GetById(request.EntradaProductoId);

            if (findEntrada == null)
            {
                _logger.LogError($"No se encontro el id de la entrada {request.EntradaProductoId}");
                throw new NotFoundException(nameof(EntradaProducto), request.EntradaProductoId);
            }

            _mapper.Map(request, findEntrada);

            await _entradaProductoRepository.UpdateAsync(findEntrada);

            _logger.LogInformation($"La operacion fue exitosa {request.EntradaProductoId}");
        }
    }
}
