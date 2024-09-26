using FluentValidation;

namespace AppAcademy.Application.Features.Auth.Users.Commands.LoginUser
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(l => l.UserName)
                .NotEmpty()
                .WithMessage("El nombre del usuario es obligatorio");

            RuleFor(l => l.Password)
                .NotEmpty()
                .WithMessage("El password del usuario es obligatorio");
        }
    }
}
