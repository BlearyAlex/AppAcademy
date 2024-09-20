using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Ubicaciones.Command.CreateUbicacion
{
    public class CreateUbicacionCommandHandler : IRequestHandler<CreateUbicacionCommand, string>
    {
        private readonly IUbicacionRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateUbicacionCommandHandler> _logger;

        public CreateUbicacionCommandHandler(IUbicacionRepository repository, IMapper mapper, ILogger<CreateUbicacionCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> Handle(CreateUbicacionCommand request, CancellationToken cancellationToken)
        {
            var ubicacionEntity = _mapper.Map<Ubicacion>(request);
            var newUbicacion = await _repository.AddAsync(ubicacionEntity);

            _logger.LogInformation($"Proveedor {newUbicacion.UbicacionId} fue creado exitosamente");

            return newUbicacion.UbicacionId;
        }
    }
}
