using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Salidas.Command.DeleteSalida
{
    public class DeleteSalidaCommandHandler : IRequestHandler<DeleteSalidaCommand>
    {
        private readonly ISalidaRepository _salidaRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteSalidaCommandHandler> _logger;

        public DeleteSalidaCommandHandler(ISalidaRepository salidaRepository, IMapper mapper, ILogger<DeleteSalidaCommandHandler> logger)
        {
            _salidaRepository = salidaRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(DeleteSalidaCommand request, CancellationToken cancellationToken)
        {
            var deletedSalida = await _salidaRepository.GetById(request.SalidaId);
            if (deletedSalida == null)
            {
                _logger.LogError($"{request.SalidaId} salida no existe en el sistema");
                throw new NotFoundException(nameof(deletedSalida), request.SalidaId);
            }

            await _salidaRepository.DeleteAsync(deletedSalida);
            _logger.LogInformation($" {request.SalidaId} fue eliminado con exito");

            return;
        }
    }
}
