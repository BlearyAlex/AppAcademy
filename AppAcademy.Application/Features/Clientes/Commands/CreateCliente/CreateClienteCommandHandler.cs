using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Clientes.Commands.CreateCliente
{
    public class CreateClienteCommandHandler : IRequestHandler<CreateClienteCommand, string>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateClienteCommandHandler> _logger;

        public CreateClienteCommandHandler(IClienteRepository clienteRepository, IMapper mapper, ILogger<CreateClienteCommandHandler> logger)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
        {
            var clienteEntity = _mapper.Map<Cliente>(request);
            var newCliente = await _clienteRepository.AddAsync(clienteEntity);

            _logger.LogInformation($"Cliente {newCliente.ClienteId} fue creado exitosamente");

            return newCliente.ClienteId;
        }
    }
}
