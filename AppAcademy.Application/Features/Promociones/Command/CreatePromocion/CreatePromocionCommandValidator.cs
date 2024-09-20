using FluentValidation;

namespace AppAcademy.Application.Features.Promociones.Command.CreatePromocion
{
    public class CreatePromocionCommandValidator : AbstractValidator<CreatePromocionCommand>
    {
        public CreatePromocionCommandValidator()
        {
            RuleFor(x => x.Nombre)
              .NotEmpty().WithMessage("El nombre de la promoción no puede estar vacío.")
              .MaximumLength(100).WithMessage("El nombre de la promoción no puede exceder los 100 caracteres.");

            RuleFor(x => x.Descripcion)
                .MaximumLength(250).WithMessage("La descripción no puede exceder los 250 caracteres.");

            RuleFor(x => x.FechaInicio)
                .LessThan(x => x.FechaFin).WithMessage("La fecha de inicio debe ser anterior a la fecha de fin.");

            RuleFor(x => x.Descuento)
                .InclusiveBetween(0, 100).WithMessage("El descuento debe estar entre 0 y 100.");
        }
    }
}
