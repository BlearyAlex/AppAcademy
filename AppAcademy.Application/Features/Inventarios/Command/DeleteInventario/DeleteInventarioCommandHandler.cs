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

namespace AppAcademy.Application.Features.Inventarios.Command.DeleteInventario
{
    public class DeleteInventarioCommandHandler : IRequestHandler<DeleteInventarioCommand>
    {
        private readonly IInventarioRepository _repository;
        private readonly ILogger<DeleteInventarioCommandHandler> _logger;

        public DeleteInventarioCommandHandler(IInventarioRepository repository, ILogger<DeleteInventarioCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task Handle(DeleteInventarioCommand request, CancellationToken cancellationToken)
        {
            var findInvenatrio = await _repository.GetById(request.InventarioId);
            if (findInvenatrio == null)
            {
                throw new NotFoundException(nameof(findInvenatrio), request.InventarioId);
            }

            await _repository.DeleteAsync(findInvenatrio);
            _logger.LogInformation($"El {request.InventarioId} fue eliminado con exito");

            return;
        }
    }
}
