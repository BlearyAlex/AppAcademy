using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Clientes.Commands.DeleteCliente
{
    public class DeleteClienteCommandHandler : IRequestHandler<DeleteClienteCommand>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteClienteCommandHandler> _logger;

        public DeleteClienteCommandHandler(IClienteRepository clienteRepository, IMapper mapper, ILogger<DeleteClienteCommandHandler> logger)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(DeleteClienteCommand request, CancellationToken cancellationToken)
        {
            var deletedCliente = await _clienteRepository.GetById(request.ClienteId);
            if (deletedCliente == null) 
            {
                _logger.LogError($"{request.ClienteId} cliente no existe en el sistema");
                throw new NotFoundException(nameof(deletedCliente), request.ClienteId);
            }

            await _clienteRepository.DeleteAsync(deletedCliente);
            _logger.LogInformation($" {request.ClienteId} fue eliminado con exito");

            return;
        }
    }
}
