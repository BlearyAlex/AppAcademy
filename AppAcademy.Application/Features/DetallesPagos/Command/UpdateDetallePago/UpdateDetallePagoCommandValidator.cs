using FluentValidation;

namespace AppAcademy.Application.Features.DetallesPagos.Command.UpdateDetallePago
{
    public class UpdateDetallePagoCommandValidator : AbstractValidator<UpdateDetallePagoCommand>
    {
        public UpdateDetallePagoCommandValidator()
        {
            RuleFor(c => c.Monto)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El total del monto debe ser mayor o igual a 0.");
        }
    }
}
