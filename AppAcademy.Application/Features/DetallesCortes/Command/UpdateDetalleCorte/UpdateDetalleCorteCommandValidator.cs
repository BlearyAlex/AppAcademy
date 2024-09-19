using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.DetallesCortes.Command.UpdateDetalleCorte
{
    public class UpdateDetalleCorteCommandValidator : AbstractValidator<UpdateDetalleCorteCommand>
    {
        public UpdateDetalleCorteCommandValidator()
        {
            RuleFor(d => d.Monto)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El monto debe ser mayor o igual a 0");
        }
    }
}
