using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Ubicaciones.Command.DeleteUbicacion
{
    public class DeleteUbicacionCommandHandler : IRequestHandler<DeleteUbicacionCommand>
    {
        private readonly IUbicacionRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteUbicacionCommandHandler> _logger;

        public DeleteUbicacionCommandHandler(IUbicacionRepository repository, IMapper mapper, ILogger<DeleteUbicacionCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(DeleteUbicacionCommand request, CancellationToken cancellationToken)
        {
            var deleteUbicacion = await _repository.GetById(request.UbicacionId);
            if (deleteUbicacion == null)
            {
                _logger.LogError($"{request.UbicacionId} ubicacion no existe en el sistma");
                throw new NotFoundException(nameof(deleteUbicacion), request.UbicacionId);
            }

            await _repository.DeleteAsync(deleteUbicacion);
            _logger.LogInformation($"El {request.UbicacionId} fue eliminado con exito");

            return;
        }
    }
}
