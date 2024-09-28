using AppAcademy.Application.DTOs;
using AppAcademy.Domain.Auth;

namespace AppAcademy.Application.Contracts.Persistence.Auth
{
    public interface IRolesRepository : IAsyncRepository<Rol>
    {
        Task AssignPermissionsToRole(string rolId, List<string> permissionIds);
        Task CreateRolWithPermissions(RolDto rolDto);
        Task UpdateRolePermissions(string rolId, List<string> newPermissionIds);
        Task DeleteRolWithPermissions(string rolId);
    }
}
