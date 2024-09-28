using AppAcademy.Application.Contracts.Persistence.Auth;
using MediatR;

namespace AppAcademy.Application.Features.Auth.Roles.Commands.DeleteRol
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRolCommand>
    {
        private readonly IRolesRepository _rolesRepository;

        public DeleteRoleCommandHandler(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        public async Task Handle(DeleteRolCommand request, CancellationToken cancellationToken)
        {
            var rol = await _rolesRepository.GetById(request.RolId);
            if (rol == null) throw new Exception("Rol no encontrado");

            await _rolesRepository.DeleteRolWithPermissions(request.RolId);
        }
    }
}
