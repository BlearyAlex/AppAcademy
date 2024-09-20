using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.DetallesPagos.Command.CreateDetallePago
{
    public class CreateDetallePagoCommandHandler : IRequestHandler<CreateDetallePagoCommand, string>
    {
        private readonly IDetallePagoRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateDetallePagoCommandHandler> _logger;

        public CreateDetallePagoCommandHandler(IDetallePagoRepository repository, IMapper mapper, ILogger<CreateDetallePagoCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> Handle(CreateDetallePagoCommand request, CancellationToken cancellationToken)
        {
            var detallePagoEnity = _mapper.Map<DetallePago>(request);
            var newDetallePago = await _repository.AddAsync(detallePagoEnity);

            _logger.LogInformation($"Corte {newDetallePago.DetallePagoId} fue creado exitosamente");

            return newDetallePago.DetallePagoId;
        }
    }
}
