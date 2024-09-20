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

namespace AppAcademy.Application.Features.Promociones.Command.UpdatePromocion
{
    public class UpdatePromocionCommandHandler : IRequestHandler<UpdatePromocionCommand>
    {
        private readonly IPromocionRepository _promocionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePromocionCommandHandler> _logger;

        public UpdatePromocionCommandHandler(IPromocionRepository promocionRepository, IMapper mapper, ILogger<UpdatePromocionCommandHandler> logger)
        {
            _promocionRepository = promocionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(UpdatePromocionCommand request, CancellationToken cancellationToken)
        {
            var findPromocion = await _promocionRepository.GetById(request.PromocionId);

            if (findPromocion == null)
            {
                _logger.LogError($"No se encontro el id de la promocion {request.PromocionId}");
                throw new NotFoundException(nameof(Promocion), request.PromocionId);
            }

            _mapper.Map(request, findPromocion);

            await _promocionRepository.UpdateAsync(findPromocion);

            _logger.LogInformation($"La operacion fue exitosa {request.PromocionId}");
        }
    }
}
