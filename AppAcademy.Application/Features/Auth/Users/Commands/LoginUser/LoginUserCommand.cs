using AppAcademy.Application.DTOs;
using MediatR;

namespace AppAcademy.Application.Features.Auth.Users.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<UserDto>
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
