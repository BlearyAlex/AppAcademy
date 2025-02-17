using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Proveedores.Commands.CreateProveedor
{
    public class CreateProveedorCommandHandler : IRequestHandler<CreateProveedorCommand, string>
    {
        private readonly IProveedorRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProveedorCommandHandler> _logger;

        public CreateProveedorCommandHandler(IProveedorRepository repository, IMapper mapper, ILogger<CreateProveedorCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> Handle(CreateProveedorCommand request, CancellationToken cancellationToken)
        {
            var proveedor = new Proveedor
            {
                Nombre = request.Nombre,
                Color = request.Color,
                Description = request.Description,
                FechaRegistro = DateTime.Now
            };

            var newProveedor = await _repository.AddAsync(proveedor);

            _logger.LogInformation($"Proveedor {newProveedor.ProveedorId} fue creado exitosamente");

            return newProveedor.ProveedorId;
        }
    }
}
