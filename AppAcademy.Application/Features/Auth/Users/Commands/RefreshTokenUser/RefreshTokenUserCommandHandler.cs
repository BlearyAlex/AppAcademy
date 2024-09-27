using AppAcademy.Application.Contracts.Persistence.Auth;
using AppAcademy.Application.DTOs;
using MediatR;

namespace AppAcademy.Application.Features.Auth.Users.Commands.RefreshTokenUser
{
    public class RefreshTokenUserCommandHandler : IRequestHandler<RefreshTokenUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public RefreshTokenUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(RefreshTokenUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.RefreshTokenAsync(request.RefreshToken);
            return result;
        }
    }
}
