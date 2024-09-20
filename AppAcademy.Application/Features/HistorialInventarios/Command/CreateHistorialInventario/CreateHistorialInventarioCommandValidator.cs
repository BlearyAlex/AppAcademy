using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.HistorialInventarios.Command.CreateHistorialInventario
{
    public class CreateHistorialInventarioCommandValidator : AbstractValidator<CreateHistorialInventarioCommand>
    {
        public CreateHistorialInventarioCommandValidator()
        {
            RuleFor(x => x.CantidadAnterior)
               .GreaterThanOrEqualTo(0).WithMessage("La cantidad anterior debe ser mayor o igual a cero.");

            RuleFor(x => x.CantidadNueva)
                .GreaterThanOrEqualTo(0).WithMessage("La cantidad nueva debe ser mayor o igual a cero.");

            RuleFor(x => x.FechaCambio)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de cambio no puede ser en el futuro.");

            RuleFor(x => x.Motivo)
                .MaximumLength(200).WithMessage("El motivo no puede exceder los 200 caracteres.");
        }
    }
}
