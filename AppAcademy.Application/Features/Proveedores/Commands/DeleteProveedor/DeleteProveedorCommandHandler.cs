using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Proveedores.Commands.DeleteProveedor
{
    public class DeleteProveedorCommandHandler : IRequestHandler<DeleteProveedorCommand>
    {
        private readonly IProveedorRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteProveedorCommandHandler> _logger;

        public DeleteProveedorCommandHandler(IProveedorRepository repository, IMapper mapper, ILogger<DeleteProveedorCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(DeleteProveedorCommand request, CancellationToken cancellationToken)
        {
            var deletedProveedor = await _repository.GetById(request.ProveedorId);
            if(deletedProveedor == null)
            {
                _logger.LogError($"{request.ProveedorId} proveedor no existe en el sistema");
                throw new NotFoundException(nameof(deletedProveedor), request.ProveedorId);
            }

            await _repository.DeleteAsync(deletedProveedor);
            _logger.LogInformation($" {request.ProveedorId} fue eliminado con exito");

            return;
        }
    }
}
