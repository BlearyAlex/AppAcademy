using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Cortes.Commands.UpdateCorte
{
    public class UpdateCorteCommandValidator : AbstractValidator<UpdateCorteCommand>
    {
        public UpdateCorteCommandValidator()
        {
            RuleFor(c => c.TotalEfectivo)
               .GreaterThanOrEqualTo(0)
               .WithMessage("El total en efectivo debe ser mayor o igual a 0.");

            RuleFor(c => c.TotalTarjeta)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El total en tarjeta debe ser mayor o igual a 0.");

            RuleFor(c => c.TotalVales)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El total en vales debe ser mayor o igual a 0.");

            RuleFor(c => c.TotalDevoluciones)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El total en devoluciones debe ser mayor o igual a 0.");

            RuleFor(c => c.Comentarios)
                .MaximumLength(500)
                .WithMessage("Los comentarios no pueden exceder los 500 caracteres.");
        }
    }
}
