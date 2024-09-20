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

namespace AppAcademy.Application.Features.Entradas.Command.UpdateEntrada
{
    public class UpdateEntradaCommandHandler : IRequestHandler<UpdateEntradaCommand>
    {
        private readonly IEntradaRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateEntradaCommandHandler> _logger;

        public UpdateEntradaCommandHandler(IEntradaRepository repository, IMapper mapper, ILogger<UpdateEntradaCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(UpdateEntradaCommand request, CancellationToken cancellationToken)
        {
            var findEntrada = await _repository.GetById(request.EntradaId);

            if (findEntrada == null)
            {
                _logger.LogError($"No se encontro el id de la entrada {request.EntradaId}");
                throw new NotFoundException(nameof(Entrada), request.EntradaId);
            }

            _mapper.Map(request, findEntrada);

            await _repository.UpdateAsync(findEntrada);

            _logger.LogInformation($"La operacion fue exitosa {request.EntradaId}");
        }
    }
}
