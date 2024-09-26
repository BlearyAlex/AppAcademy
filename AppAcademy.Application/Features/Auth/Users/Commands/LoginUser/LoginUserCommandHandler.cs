using AppAcademy.Application.Contracts.Persistence.Auth;
using AppAcademy.Application.DTOs;
using AppAcademy.Domain.Auth;
using MediatR;

namespace AppAcademy.Application.Features.Auth.Users.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public LoginUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Login(request);
            return user;
        }
    }
}
