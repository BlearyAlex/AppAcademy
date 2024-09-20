using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Salidas.Command.CreateSalida
{
    public class CreateSalidaCommandValidator : AbstractValidator<CreateSalidaCommand>
    {
        public CreateSalidaCommandValidator()
        {
            RuleFor(s => s.FechaSalida)
               .NotEmpty().WithMessage("La fecha de salida es obligatoria.")
               .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de salida no puede ser en el futuro.");

            RuleFor(s => s.TotalProductosSalida)
                .GreaterThanOrEqualTo(0).WithMessage("El total de productos en salida debe ser mayor o igual a cero.");

            RuleFor(s => s.Comentarios)
                .MaximumLength(500).WithMessage("Los comentarios no pueden exceder los 500 caracteres.")
                .When(s => !string.IsNullOrEmpty(s.Comentarios)); // Solo validar si hay comentarios
        }
    }
}
