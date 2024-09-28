using MediatR;

namespace AppAcademy.Application.Features.Auth.Roles.Commands.CreatePermisionWithRole
{
    public class CreatePermisionWithRoleCommand : IRequest
    {
        public string RoleId { get; set; }
        public List<string> PermissionIds { get; set; }
    }
}
