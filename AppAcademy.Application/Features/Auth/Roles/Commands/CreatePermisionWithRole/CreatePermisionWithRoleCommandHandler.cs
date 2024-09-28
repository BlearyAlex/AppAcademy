using AppAcademy.Application.Contracts.Persistence.Auth;
using MediatR;

namespace AppAcademy.Application.Features.Auth.Roles.Commands.CreatePermisionWithRole
{
    public class CreatePermisionWithRoleCommandHandler : IRequestHandler<CreatePermisionWithRoleCommand>
    {
        private readonly IRolesRepository _rolesRepository;

        public CreatePermisionWithRoleCommandHandler(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        public async Task Handle(CreatePermisionWithRoleCommand request, CancellationToken cancellationToken)
        {
            await _rolesRepository.AssignPermissionsToRole(request.RoleId, request.PermissionIds);
        }
    }
}
