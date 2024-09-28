using MediatR;

namespace AppAcademy.Application.Features.Auth.Roles.Commands.DeleteRol
{
    public class DeleteRolCommand : IRequest
    {
        public string RolId { get; set; }
    }
}
