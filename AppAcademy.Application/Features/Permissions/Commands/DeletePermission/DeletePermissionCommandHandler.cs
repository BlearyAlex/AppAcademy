using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Permissions.Commands.DeletePermission
{
    public class DeletePermissionCommandHandler : IRequestHandler<DeletePermissionCommand>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly ILogger<DeletePermissionCommandHandler> _logger;

        public DeletePermissionCommandHandler(IPermissionRepository permissionRepository, ILogger<DeletePermissionCommandHandler> logger)
        {
            _permissionRepository = permissionRepository;
            _logger = logger;
        }

        public async Task Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
        {
            var findPermission = await _permissionRepository.GetByIdInt(request.PermissionId);
            if (findPermission == null)
            {
                _logger.LogError($"{request.PermissionId} permiso no existe en el sistema");
                throw new NotFoundException(nameof(findPermission), request.PermissionId);
            }

            await _permissionRepository.DeleteAsync(findPermission);

            return;
        }
    }
}
