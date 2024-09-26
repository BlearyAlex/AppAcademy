using AppAcademy.Application.DTOs;
using AppAcademy.Application.Features.Auth.Users.Commands.CreateUser;
using AppAcademy.Application.Features.Auth.Users.Commands.LoginUser;
using AppAcademy.Domain.Auth;

namespace AppAcademy.Application.Contracts.Persistence.Auth
{
    public interface IUserRepository
    {
        public string CrearToken(User user);
        Task<UserDto> RegisterUser(CreateUserCommand createUserCommand);
        Task<UserDto> Login(LoginUserCommand loginUserCommand);
    }
}
