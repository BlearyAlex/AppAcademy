using FluentValidation;

namespace AppAcademy.Application.Features.Clientes.Commands.CreateCliente
{
    public class CreateClienteCommandValidator : AbstractValidator<CreateClienteCommand>
    {
        public CreateClienteCommandValidator()
        {
            RuleFor(p => p.NombreCompleto)
                .NotEmpty()
                .WithMessage("El campo no puede ir vacio");

            RuleFor(p => p.Email)
                .EmailAddress().WithMessage("El formato de email no es valido");

            RuleFor(p => p.Telefono)
                .Matches(@"^\+?\d{10,15}$").WithMessage("El número de teléfono no es válido.");
        }
    }
}
