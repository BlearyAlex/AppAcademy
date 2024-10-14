using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Entradas.Commands.CreateEntrada
{
    public class CreateEntradaCommandValidator : AbstractValidator<CreateEntradaCommand>
    {
        public CreateEntradaCommandValidator()
        {
            RuleFor(entrada => entrada.TotalProductosEntrada)
          .GreaterThan(0).WithMessage("El {TotalProductosEntrada} en entrada debe ser mayor que 0.");

            RuleFor(entrada => entrada.FechaDeEntrega)
                .GreaterThanOrEqualTo(DateTime.Now)
                .WithMessage("La {PropertyName} debe ser en el futuro o igual a la fecha y hora actual.");

            RuleFor(entrada => entrada.NumeroFactura)
                .NotEmpty().WithMessage("El {NumeroFactura} no puede estar vacío.")
                .MaximumLength(50).WithMessage("El número de factura no puede tener más de 50 caracteres.");

            RuleFor(entrada => entrada.VencimientoPago)
                .GreaterThanOrEqualTo(DateTime.Now)
                .WithMessage("La {PropertyName} debe ser en el futuro o igual a la fecha y hora actual.");

            RuleFor(entrada => entrada.Folio)
                .MaximumLength(50).WithMessage("El {Folio} no puede tener más de 50 caracteres.")
                .When(entrada => !string.IsNullOrEmpty(entrada.Folio));

            RuleFor(entrada => entrada.Bruto)
                .GreaterThan(0).WithMessage("El monto {Bruto} debe ser mayor que 0.");
        }
    }
}
