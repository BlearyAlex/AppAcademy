using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.HistorialInventarios.Command.DeleteHistorialInventario
{
    public class DeleteHistorialInventarioCommandHandler : IRequestHandler<DeleteHistorialInventarioCommand>
    {
        private readonly IHistorialInventarioRepository _repository;    
        private readonly ILogger<DeleteHistorialInventarioCommandHandler> _logger;

        public DeleteHistorialInventarioCommandHandler(IHistorialInventarioRepository repository, ILogger<DeleteHistorialInventarioCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task Handle(DeleteHistorialInventarioCommand request, CancellationToken cancellationToken)
        {
            var findHistorialInv = await _repository.GetById(request.HistorialInventarioId);
            if (findHistorialInv == null)
            {
                throw new NotFoundException(nameof(findHistorialInv), request.HistorialInventarioId);
            }

            await _repository.DeleteAsync(findHistorialInv);
            _logger.LogInformation($"El {request.HistorialInventarioId} fue eliminado con exito");

            return;
        }
    }
}
