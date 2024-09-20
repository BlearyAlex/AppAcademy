using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Salidas.Command.CreateSalida
{
    public class CreateSalidaCommandHandler : IRequestHandler<CreateSalidaCommand, string>
    {
        private readonly ISalidaRepository _salidaRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateSalidaCommandHandler> _logger;

        public CreateSalidaCommandHandler(ISalidaRepository salidaRepository, IMapper mapper, ILogger<CreateSalidaCommandHandler> logger)
        {
            _salidaRepository = salidaRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> Handle(CreateSalidaCommand request, CancellationToken cancellationToken)
        {
            var salidaEntity = _mapper.Map<Salida>(request);
            var newSalida = await _salidaRepository.AddAsync(salidaEntity);

            _logger.LogInformation($"Proveedor {newSalida.SalidaId} fue creado exitosamente");

            return newSalida.SalidaId;
        }
    }
}
