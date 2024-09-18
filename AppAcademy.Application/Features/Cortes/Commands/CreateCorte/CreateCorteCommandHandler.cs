using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Cortes.Commands.CreateCorte
{
    public class CreateCorteCommandHandler : IRequestHandler<CreateCorteCommand, string>
    {
        private readonly ICorteRepository _corteRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCorteCommandHandler> _logger;

        public CreateCorteCommandHandler(ICorteRepository corteRepository, IMapper mapper, ILogger<CreateCorteCommandHandler> logger)
        {
            _corteRepository = corteRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> Handle(CreateCorteCommand request, CancellationToken cancellationToken)
        {
            var corteEntity = _mapper.Map<Corte>(request);
            var newCorte = await _corteRepository.AddAsync(corteEntity);

            _logger.LogInformation($"Corte {newCorte.CorteId} fue creado exitosamente");

            return newCorte.CorteId;
        }
    }
}
