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

namespace AppAcademy.Application.Features.Devoluciones.Command.UpdateDevolucion
{
    public class UpdateDevolucionCommandHandler : IRequestHandler<UpdateDevolucionCommand>
    {
        private readonly IDevolucionRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateDevolucionCommandHandler> _logger;

        public UpdateDevolucionCommandHandler(IDevolucionRepository repository, IMapper mapper, ILogger<UpdateDevolucionCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(UpdateDevolucionCommand request, CancellationToken cancellationToken)
        {
            var findDevolucion = await _repository.GetById(request.DevolucionId);

            if (findDevolucion == null)
            {
                _logger.LogError($"No se encontro el id de la devolucion {request.DevolucionId}");
                throw new NotFoundException(nameof(Devolucion), request.DevolucionId);
            }

            _mapper.Map(request, findDevolucion);

            await _repository.UpdateAsync(findDevolucion);

            _logger.LogInformation($"La operacion fue exitosa {request.DevolucionId}");
        }
    }
}
