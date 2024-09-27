using MediatR;

namespace AppAcademy.Application.Features.Auth.Roles.Commands.CreateRol
{
    public class CreateRolCommand : IRequest
    {
        public string NameRol { get; set; }
        public List<string> Permisos { get; set; }
    }
}
