using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Application.Exceptions;
using AppAcademy.Domain.ControlAcademia;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Permissions.Commands.UpdatePermission
{
    public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly ILogger<UpdatePermissionCommandHandler> _logger;

        public UpdatePermissionCommandHandler(IPermissionRepository permissionRepository, ILogger<UpdatePermissionCommandHandler> logger)
        {
            _permissionRepository = permissionRepository;
            _logger = logger;
        }

        public async Task Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {
            var findPermission = await _permissionRepository.GetByIdInt(request.PermissionId);
            if (findPermission == null)
            {
                _logger.LogError($"No se encontro el id del permiso {request.PermissionId}");
                throw new NotFoundException(nameof(Permission), request.PermissionId);
            }

            findPermission.Nombre = request.Nombre;
            findPermission.Descripcion = request.Descripcion;
            findPermission.Activo = request.Activo;

            await _permissionRepository.UpdateAsync(findPermission);

            _logger.LogInformation($"La operacion fue exitosa {request.PermissionId}");
        }
    }
}
