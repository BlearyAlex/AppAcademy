using FluentValidation;

namespace AppAcademy.Application.Features.Auth.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(u => u.UserName)
                .NotEmpty()
                .WithMessage("El campo del nombre del usuario no puede ir vacio");

            RuleFor(u => u.Email)
                .EmailAddress()
                .WithMessage("El formato de email no es valido");

            RuleFor(u => u.Password)
                .NotEmpty()
                .WithMessage("El campo password no puede ir vacio");
        }
    }
}
