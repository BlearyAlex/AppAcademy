using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Promociones.Command.DeletePromocion
{
    public class DeletePromocionCommandHandler : IRequestHandler<DeletePromocionCommand>
    {
        private readonly IPromocionRepository _promocionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeletePromocionCommandHandler> _logger;

        public DeletePromocionCommandHandler(IPromocionRepository promocionRepository, IMapper mapper, ILogger<DeletePromocionCommandHandler> logger)
        {
            _promocionRepository = promocionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(DeletePromocionCommand request, CancellationToken cancellationToken)
        {
            var deletePromocion = await _promocionRepository.GetById(request.PromocionId);
            if (deletePromocion == null)
            {
                _logger.LogError($"{request.PromocionId} promocion no existe en el sistema");
                throw new NotFoundException(nameof(deletePromocion), request.PromocionId);
            }

            await _promocionRepository.DeleteAsync(deletePromocion);
            _logger.LogInformation($"El {request.PromocionId} fue eliminado con exito");

            return;
        }
    }
}
