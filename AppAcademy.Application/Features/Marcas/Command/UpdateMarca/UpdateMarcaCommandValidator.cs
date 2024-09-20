using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Marcas.Command.UpdateMarca
{
    public class UpdateMarcaCommandValidator : AbstractValidator<UpdateMarcaCommand>
    {
        public UpdateMarcaCommandValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre de la marca no puede estar vacío.")
                .MaximumLength(100).WithMessage("El nombre de la marca no puede exceder los 100 caracteres.");
        }
    }
}
