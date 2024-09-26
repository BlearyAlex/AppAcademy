using AppAcademy.Application.DTOs;
using MediatR;

namespace AppAcademy.Application.Features.Auth.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<UserDto>
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
