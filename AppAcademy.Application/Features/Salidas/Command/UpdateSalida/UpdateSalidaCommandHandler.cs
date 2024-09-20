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

namespace AppAcademy.Application.Features.Salidas.Command.UpdateSalida
{
    public class UpdateSalidaCommandHandler : IRequestHandler<UpdateSalidaCommand>
    {
        private readonly ISalidaRepository _salidaRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateSalidaCommandHandler> _logger;

        public UpdateSalidaCommandHandler(ISalidaRepository salidaRepository, IMapper mapper, ILogger<UpdateSalidaCommandHandler> logger)
        {
            _salidaRepository = salidaRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(UpdateSalidaCommand request, CancellationToken cancellationToken)
        {
            var findSalida = await _salidaRepository.GetById(request.SalidaId);

            if (findSalida == null)
            {
                _logger.LogError($"No se encontro el id de la salida {request.SalidaId}");
                throw new NotFoundException(nameof(Salida), request.SalidaId);
            }

            _mapper.Map(request, findSalida);

            await _salidaRepository.UpdateAsync(findSalida);

            _logger.LogInformation($"La operacion fue exitosa {request.SalidaId}");
        }
    }
}
