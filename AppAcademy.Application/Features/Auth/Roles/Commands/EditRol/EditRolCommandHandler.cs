using AppAcademy.Application.Contracts.Persistence.Auth;
using MediatR;

namespace AppAcademy.Application.Features.Auth.Roles.Commands.EditRol
{
    public class EditRolCommandHandler : IRequestHandler<EditRolCommand>
    {
        private readonly IRolesRepository _rolesRepository;

        public EditRolCommandHandler(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        public async Task Handle(EditRolCommand request, CancellationToken cancellationToken)
        {
            var rol = await _rolesRepository.GetById(request.RoleId);
            if (rol == null) throw new Exception("Rol no encontrado");

            rol.NameRol = request.NameRol;

            // Actualizar permisos del rol
            await _rolesRepository.UpdateRolePermissions(request.RoleId, request.PermissionIds);
        }
    }
}
