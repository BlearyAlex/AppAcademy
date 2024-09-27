using AppAcademy.Application.DTOs;

namespace AppAcademy.Application.Contracts.Persistence.Auth
{
    public interface IRolesRepository
    {
        Task AssignPermissionsToRole(string rolId, List<string> permissionIds);
        Task CreateRolWithPermissions(RolDto rolDto);
    }
}
