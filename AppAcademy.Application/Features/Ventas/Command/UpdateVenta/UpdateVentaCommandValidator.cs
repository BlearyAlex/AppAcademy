using AppAcademy.Application.Features.Ventas.Command.CreateVenta;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Ventas.Command.UpdateVenta
{
    public class UpdateVentaCommandValidator : AbstractValidator<CreateVentaCommand>
    {
        public UpdateVentaCommandValidator()
        {
            RuleFor(v => v.FechaCompra)
               .NotEmpty().WithMessage("La fecha de compra es obligatoria.")
               .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de compra no puede ser en el futuro.");
        }
    }
}
