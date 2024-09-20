using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Inventarios.Command.UpdateInventario
{
    public class UpdateInventarioCommandValidator : AbstractValidator<UpdateInventarioCommand>
    {
        public UpdateInventarioCommandValidator()
        {
            RuleFor(x => x.Cantidad)
               .GreaterThan(0).WithMessage("La cantidad debe ser mayor que cero.");

            RuleFor(x => x.FechaUltimaActualizacion)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de última actualización no puede ser en el futuro.");

            RuleFor(x => x.FechaExpiracion)
                .GreaterThan(DateTime.Now).WithMessage("La fecha de expiración debe ser en el futuro.");

            RuleFor(x => x.Ubicacion)
                .MaximumLength(100).WithMessage("La ubicación no puede exceder los 100 caracteres.")
                .When(x => !string.IsNullOrWhiteSpace(x.Ubicacion));

            RuleFor(x => x.Lote)
                .MaximumLength(50).WithMessage("El lote no puede exceder los 50 caracteres.")
                .When(x => !string.IsNullOrWhiteSpace(x.Lote));
        }
    }
}
