using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;


namespace AppAcademy.Application.Features.Cortes.Commands.DeleteCorte
{
    public class DeleteCorteCommandHandler : IRequestHandler<DeleteCorteCommand>
    {
        private readonly ICorteRepository _corteRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteCorteCommandHandler> _logger;

        public DeleteCorteCommandHandler(ICorteRepository corteRepository, IMapper mapper, ILogger<DeleteCorteCommandHandler> logger)
        {
            _corteRepository = corteRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(DeleteCorteCommand request, CancellationToken cancellationToken)
        {
            var deleteCorte = await _corteRepository.GetById(request.CorteId);
            if (deleteCorte == null)
            {
                _logger.LogError($"{request.CorteId} corte no existe en el sistma");
                throw new NotFoundException(nameof(deleteCorte), request.CorteId);
            }

            await _corteRepository.DeleteAsync(deleteCorte);
            _logger.LogInformation($"El {request.CorteId} fue eliminado con exito");

            return;
        }
    }
}
