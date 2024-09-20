using FluentValidation;

namespace AppAcademy.Application.Features.Ventas.Command.CreateVenta
{
    public class CreateVentaCommandValidator : AbstractValidator<CreateVentaCommand>
    {
        public CreateVentaCommandValidator()
        {
            RuleFor(v => v.FechaCompra)
                .NotEmpty().WithMessage("La fecha de compra es obligatoria.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de compra no puede ser en el futuro.");
        }
    }
}
