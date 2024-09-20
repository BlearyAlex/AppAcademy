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

namespace AppAcademy.Application.Features.EntradasProductos.Command.DeleteEntrada
{
    public class DeleteEntradaProductoCommandHandler : IRequestHandler<DeleteEntradaProductoCommand>
    {
        private readonly IEntradaProductoRepository _repository;
        private readonly ILogger<DeleteEntradaProductoCommandHandler> _logger;

        public DeleteEntradaProductoCommandHandler(IEntradaProductoRepository repository, ILogger<DeleteEntradaProductoCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task Handle(DeleteEntradaProductoCommand request, CancellationToken cancellationToken)
        {
            var findEntrada = await _repository.GetById(request.EntradaProductoId);

            if (findEntrada == null)
            {
                _logger.LogError($"{request.EntradaProductoId} Entrada Producto no existe en el sistma");
                throw new NotFoundException(nameof(findEntrada), request.EntradaProductoId);
            }

            await _repository.DeleteAsync(findEntrada);
            _logger.LogInformation($"El {request.EntradaProductoId} fue eliminado con exito");

            return;
        }
    }
}
