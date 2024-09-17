using FluentValidation;

namespace AppAcademy.Application.Features.Proveedores.Commands.UpdateProveedor
{
    public class UpdateProveedorCommandValidator : AbstractValidator<UpdateProveedorCommand>
    {
        public UpdateProveedorCommandValidator()
        {
            RuleFor(p => p.Nombre)
               .NotEmpty()
               .WithMessage("El nombre del proveedor no puede estar vacio");
        }
    }
}
