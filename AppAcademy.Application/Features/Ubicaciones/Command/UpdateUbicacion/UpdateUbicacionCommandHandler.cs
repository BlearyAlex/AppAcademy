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

namespace AppAcademy.Application.Features.Ubicaciones.Command.UpdateUbicacion
{
    public class UpdateUbicacionCommandHandler : IRequestHandler<UpdateUbicacionCommand>
    {
        private readonly IUbicacionRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateUbicacionCommandHandler> _logger;

        public UpdateUbicacionCommandHandler(IUbicacionRepository repository, IMapper mapper, ILogger<UpdateUbicacionCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(UpdateUbicacionCommand request, CancellationToken cancellationToken)
        {
            var findUbicacion = await _repository.GetById(request.UbicacionId);

            if (findUbicacion == null)
            {
                _logger.LogError($"No se encontro el id de la ubicacion {request.UbicacionId}");
                throw new NotFoundException(nameof(Ubicacion), request.UbicacionId);
            }

            _mapper.Map(request, findUbicacion);

            await _repository.UpdateAsync(findUbicacion);

            _logger.LogInformation($"No se encontro el id de la ubicacion  {request.UbicacionId}");
        }
    }
}
