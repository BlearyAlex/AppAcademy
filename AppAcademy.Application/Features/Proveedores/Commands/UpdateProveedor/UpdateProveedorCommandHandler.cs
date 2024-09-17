using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Exceptions;
using AppAcademy.Domain.PuntoDeVenta;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Proveedores.Commands.UpdateProveedor
{
    public class UpdateProveedorCommandHandler : IRequestHandler<UpdateProveedorCommand>
    {
        private readonly IProveedorRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProveedorCommandHandler> _logger;

        public UpdateProveedorCommandHandler(IProveedorRepository repository, IMapper mapper, ILogger<UpdateProveedorCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(UpdateProveedorCommand request, CancellationToken cancellationToken)
        {
            var findProveedor = await _repository.GetById(request.ProveedorId);

            if (findProveedor == null)
            {
                _logger.LogError($"No se encontro el id del proveedor {request.ProveedorId}");
                throw new NotFoundException(nameof(Proveedor), request.ProveedorId);
            }

            _mapper.Map(request, findProveedor);

            await _repository.UpdateAsync(findProveedor);

            _logger.LogInformation($"La operacion fue exitosa {request.ProveedorId}");
        }
    }
}
