﻿using FluentValidation;

namespace AppAcademy.Application.Features.Entradas.Commands.UpdateEntrada
{
    public class UpdateEntradaCommandValidator : AbstractValidator<UpdateEntradaCommand>
    {
        public UpdateEntradaCommandValidator()
        {
            RuleFor(entrada => entrada.TotalProductosEntrada)
          .GreaterThan(0).WithMessage("El total de productos en entrada debe ser mayor que 0.");

            RuleFor(entrada => entrada.FechaDeEntrega)
                .GreaterThan(DateTime.Now).WithMessage("La fecha de entrega debe ser en el futuro.");

            RuleFor(entrada => entrada.NumeroFactura)
                .NotEmpty().WithMessage("El número de factura no puede estar vacío.")
                .MaximumLength(50).WithMessage("El número de factura no puede tener más de 50 caracteres.");

            RuleFor(entrada => entrada.VencimientoPago)
                .GreaterThanOrEqualTo(DateTime.Now)
                .WithMessage("La {PropertyName} debe ser en el futuro o igual a la fecha y hora actual.");

            RuleFor(entrada => entrada.Folio)
                .MaximumLength(50).WithMessage("El folio no puede tener más de 50 caracteres.")
                .When(entrada => !string.IsNullOrEmpty(entrada.Folio));

            RuleFor(entrada => entrada.Bruto)
                .GreaterThan(0).WithMessage("El monto bruto debe ser mayor que 0.");
        }
    }
}
