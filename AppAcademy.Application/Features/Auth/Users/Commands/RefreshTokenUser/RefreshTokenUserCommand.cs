using AppAcademy.Application.DTOs;
using MediatR;

namespace AppAcademy.Application.Features.Auth.Users.Commands.RefreshTokenUser
{
    public class RefreshTokenUserCommand : IRequest<UserDto>
    {
        public string? RefreshToken { get; set; }
    }
}
