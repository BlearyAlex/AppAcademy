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

namespace AppAcademy.Application.Features.HistorialInventarios.Command.UpdateHistorialInventario
{
    public class UpdateHistorialInventarioCommandHandler : IRequestHandler<UpdateHistorialInventarioCommand>
    {
        private readonly IHistorialInventarioRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateHistorialInventarioCommandHandler> _logger;

        public UpdateHistorialInventarioCommandHandler(IHistorialInventarioRepository repository, IMapper mapper, ILogger<UpdateHistorialInventarioCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(UpdateHistorialInventarioCommand request, CancellationToken cancellationToken)
        {
            var findHistorialInv = await _repository.GetById(request.HistorialInventarioId);
            if (findHistorialInv == null)
            {
                _logger.LogError($"No se encontro el id de la entrada {request.HistorialInventarioId}");
                throw new NotFoundException(nameof(HistorialInventario), request.HistorialInventarioId);
            }

            _mapper.Map(request, findHistorialInv);

            await _repository.UpdateAsync(findHistorialInv);

            _logger.LogInformation($"La operacion fue exitosa {request.HistorialInventarioId}");
        }
    }
}
