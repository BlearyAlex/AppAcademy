using FluentValidation;

namespace AppAcademy.Application.Features.Categorias.Commands.UpdateCategoria
{
    public class UpdateCategoriaCommandValidator : AbstractValidator<UpdateCategoriaCommand>
    {
        public UpdateCategoriaCommandValidator()
        {
            RuleFor(p => p.Nombre)
                .NotEmpty()
                .WithMessage("El nombre de la categoria no puede estar vacio");
        }
    }
}
