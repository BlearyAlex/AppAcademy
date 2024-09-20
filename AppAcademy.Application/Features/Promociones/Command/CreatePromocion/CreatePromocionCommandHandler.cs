using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Promociones.Command.CreatePromocion
{
    public class CreatePromocionCommandHandler : IRequestHandler<CreatePromocionCommand, string>
    {
        private readonly IPromocionRepository _promocionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreatePromocionCommandHandler> _logger;

        public CreatePromocionCommandHandler(IPromocionRepository promocionRepository, IMapper mapper, ILogger<CreatePromocionCommandHandler> logger)
        {
            _promocionRepository = promocionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> Handle(CreatePromocionCommand request, CancellationToken cancellationToken)
        {
            var promocionEntity = _mapper.Map<Promocion>(request);
            var newPromocion = await _promocionRepository.AddAsync(promocionEntity);

            _logger.LogInformation($"Producto {newPromocion.PromocionId} fue creado exitosamente");

            return newPromocion.PromocionId;
        }
    }
}
