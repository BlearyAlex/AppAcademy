using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Domain.ControlAcademia;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Permissions.Commands.CreatePermission
{
    public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, int>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly ILogger<CreatePermissionCommand> _logger;

        public CreatePermissionCommandHandler(IPermissionRepository permissionRepository, ILogger<CreatePermissionCommand> logger)
        {
            _permissionRepository = permissionRepository;
            _logger = logger;
        }

        public async Task<int> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            var permission = new Permission
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                Activo = request.Activo,
            };

            var newPermission = await _permissionRepository.AddAsync(permission);

            return newPermission.PermissionId;
        }
    }
}
