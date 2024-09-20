using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Exceptions;
using AppAcademy.Domain.PuntoDeVenta;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Ventas.Command.UpdateVenta
{
    public class UpdateVentaCommandHandler : IRequestHandler<UpdateVentaCommand>
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateVentaCommandHandler> _logger;

        public UpdateVentaCommandHandler(IVentaRepository ventaRepository, IMapper mapper, ILogger<UpdateVentaCommandHandler> logger)
        {
            _ventaRepository = ventaRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(UpdateVentaCommand request, CancellationToken cancellationToken)
        {
            var findVenta = await _ventaRepository.GetById(request.ventaId);

            if (findVenta == null)
            {
                _logger.LogError($"No se encontro el id de la venta {request.ventaId}");
                throw new NotFoundException(nameof(Venta), request.ventaId);
            }

            _mapper.Map(request, findVenta);

            await _ventaRepository.UpdateAsync(findVenta);

            _logger.LogInformation($"La operacion fue exitosa {request.ventaId}");
        }
    }
}
