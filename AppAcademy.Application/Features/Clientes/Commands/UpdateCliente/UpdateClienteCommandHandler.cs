using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Exceptions;
using AppAcademy.Domain.PuntoDeVenta;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Clientes.Commands.UpdateCliente
{
    public class UpdateClienteCommandHandler : IRequestHandler<UpdateClienteCommand>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateClienteCommandHandler> _logger;

        public UpdateClienteCommandHandler(IClienteRepository clienteRepository, IMapper mapper, ILogger<UpdateClienteCommandHandler> logger)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(UpdateClienteCommand request, CancellationToken cancellationToken)
        {
            var findCliente = await _clienteRepository.GetById(request.ClienteId);
            if(findCliente == null)
            {
                _logger.LogError($"No se encontro el id del cliente {request.ClienteId}");
                throw new NotFoundException(nameof(Categoria), request.ClienteId);
            }

            _mapper.Map(request, findCliente);

            await _clienteRepository.UpdateAsync(findCliente);

            _logger.LogInformation($"La operacion fue exitosa {request.ClienteId}");
        }
    }
}
