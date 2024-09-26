using AppAcademy.Application.Contracts.Persistence.Auth;
using AppAcademy.Application.DTOs;
using MediatR;

namespace AppAcademy.Application.Features.Auth.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userDto = await _userRepository.RegisterUser(request);
            return userDto;
        }
    }
}
