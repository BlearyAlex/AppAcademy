using FluentValidation;

namespace AppAcademy.Application.Features.DetallesPagos.Command.CreateDetallePago
{
    public class CreateDetallePagoCommandValidator : AbstractValidator<CreateDetallePagoCommand>
    {
        public CreateDetallePagoCommandValidator()
        {
            RuleFor(c => c.Monto)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El total del monto debe ser mayor o igual a 0.");
        }
    }
}
