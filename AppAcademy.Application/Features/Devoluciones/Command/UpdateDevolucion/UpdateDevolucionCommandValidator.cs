using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Devoluciones.Command.UpdateDevolucion
{
    public class UpdateDevolucionCommandValidator : AbstractValidator<UpdateDevolucionCommand>
    {
        public UpdateDevolucionCommandValidator()
        {
            RuleFor(devolucion => devolucion.Cantidad)
           .GreaterThan(0).WithMessage("La cantidad debe ser mayor que 0.");

            RuleFor(devolucion => devolucion.Motivo)
                .NotEmpty().WithMessage("El motivo no puede estar vacío.")
                .MaximumLength(200).WithMessage("El motivo no puede tener más de 200 caracteres.");

            RuleFor(devolucion => devolucion.FechaDevolucion)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de devolución no puede ser en el futuro.");
        }
    }
}
