using MediatR;

namespace AppAcademy.Application.Features.Auth.Roles.Commands.EditRol
{
    public class EditRolCommand : IRequest
    {
        public string RoleId { get; set; }
        public string NameRol { get; set; }
        public List<string> PermissionIds { get; set; }
    }
}
