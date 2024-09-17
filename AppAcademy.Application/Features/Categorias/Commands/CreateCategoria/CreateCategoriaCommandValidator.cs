using FluentValidation;

namespace AppAcademy.Application.Features.Categorias.Commands.CreateCategoria
{
    public class CreateCategoriaCommandValidator : AbstractValidator<CreateCategoriaCommand>
    {
        public CreateCategoriaCommandValidator()
        {
            RuleFor(p => p.Nombre)
                .NotEmpty()
                .WithMessage("El nombre de la categoria no puede estar vacio");
        }
    }
}
