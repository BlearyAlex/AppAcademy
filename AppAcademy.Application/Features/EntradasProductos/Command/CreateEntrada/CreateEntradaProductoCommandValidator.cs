using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.EntradasProductos.Command.CreateEntrada
{
    public class CreateEntradaProductoCommandValidator : AbstractValidator<CreateEntradaProductoCommand>
    {
        public CreateEntradaProductoCommandValidator()
        {
            RuleFor(e => e.Cantidad)
                .GreaterThanOrEqualTo(0).WithMessage("La cantidad debe ser mayor que 0");

            RuleFor(e => e.Costo)
                .GreaterThanOrEqualTo(0).WithMessage("El costo debe ser mayor que 0");
        }
    }
}
