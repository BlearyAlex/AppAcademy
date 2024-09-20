using AppAcademy.Application.Features.Ubicaciones.Command.CreateUbicacion;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Ubicaciones.Command.UpdateUbicacion
{
    public class UpdateUbicacionCommandValidator : AbstractValidator<CreateUbicacionCommand>
    {
        public UpdateUbicacionCommandValidator()
        {
            RuleFor(u => u.Nombre)
               .NotEmpty().WithMessage("El nombre de la ubicación es obligatorio.")
               .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

            RuleFor(u => u.Direccion)
                .NotEmpty().WithMessage("La dirección es obligatoria.")
                .MaximumLength(200).WithMessage("La dirección no puede exceder los 200 caracteres.");
        }
    }
}
