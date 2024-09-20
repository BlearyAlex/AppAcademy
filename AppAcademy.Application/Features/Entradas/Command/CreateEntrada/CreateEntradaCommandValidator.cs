using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Entradas.Command.CreateEntrada
{
    public class CreateEntradaCommandValidator : AbstractValidator<CreateEntradaCommand>
    {
        public CreateEntradaCommandValidator()
        {
            RuleFor(entrada => entrada.TotalProductosEntrada)
          .GreaterThan(0).WithMessage("El total de productos en entrada debe ser mayor que 0.");

            RuleFor(entrada => entrada.FechaDeEntrega)
                .GreaterThan(DateTime.Now).WithMessage("La fecha de entrega debe ser en el futuro.");

            RuleFor(entrada => entrada.NumeroFactura)
                .NotEmpty().WithMessage("El número de factura no puede estar vacío.")
                .MaximumLength(50).WithMessage("El número de factura no puede tener más de 50 caracteres.");

            RuleFor(entrada => entrada.VencimientoPago)
                .GreaterThan(0).WithMessage("El vencimiento de pago debe ser mayor que 0.");

            RuleFor(entrada => entrada.Folio)
                .MaximumLength(50).WithMessage("El folio no puede tener más de 50 caracteres.")
                .When(entrada => !string.IsNullOrEmpty(entrada.Folio));

            RuleFor(entrada => entrada.Bruto)
                .GreaterThan(0).WithMessage("El monto bruto debe ser mayor que 0.");
        }
    }
}
