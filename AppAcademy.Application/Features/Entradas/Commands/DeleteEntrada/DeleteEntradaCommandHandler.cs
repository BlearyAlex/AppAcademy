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

namespace AppAcademy.Application.Features.Entradas.Commands.DeleteEntrada
{
    public class DeleteEntradaCommandHandler : IRequestHandler<DeleteEntradaCommand>
    {
        private readonly IEntradaRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteEntradaCommandHandler> _logger;

        public DeleteEntradaCommandHandler(IEntradaRepository repository, IMapper mapper, ILogger<DeleteEntradaCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(DeleteEntradaCommand request, CancellationToken cancellationToken)
        {
            var findEntrada = await _repository.GetById(request.EntradaId);
            if (findEntrada == null)
            {
                _logger.LogError($"{request.EntradaId} Entrada no existe en el sistma");
                throw new NotFoundException(nameof(findEntrada), request.EntradaId);
            }

            await _repository.DeleteAsync(findEntrada);
            _logger.LogInformation($"El {request.EntradaId} fue eliminado con exito");

            return;
        }
    }
}
