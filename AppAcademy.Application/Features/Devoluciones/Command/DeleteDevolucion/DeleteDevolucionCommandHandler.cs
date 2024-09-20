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

namespace AppAcademy.Application.Features.Devoluciones.Command.DeleteDevolucion
{
    public class DeleteDevolucionCommandHandler : IRequestHandler<DeleteDevolucionCommand>
    {
        private readonly IDevolucionRepository _devolucionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteDevolucionCommandHandler> _logger;

        public DeleteDevolucionCommandHandler(IDevolucionRepository devolucionRepository, IMapper mapper, ILogger<DeleteDevolucionCommandHandler> logger)
        {
            _devolucionRepository = devolucionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(DeleteDevolucionCommand request, CancellationToken cancellationToken)
        {
            var findDevolucion = await _devolucionRepository.GetById(request.DevolucionId);
            if (findDevolucion == null)
            {
                _logger.LogError($"{request.DevolucionId} Devolucion no existe en el sistma");
                throw new NotFoundException(nameof(findDevolucion), request.DevolucionId);
            }

            await _devolucionRepository.DeleteAsync(findDevolucion);
            _logger.LogInformation($"El {request.DevolucionId} fue eliminado con exito");

            return;
        }
    }
}
