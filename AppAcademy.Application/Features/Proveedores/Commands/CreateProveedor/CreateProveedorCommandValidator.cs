using FluentValidation;

namespace AppAcademy.Application.Features.Proveedores.Commands.CreateProveedor
{
    public class CreateProveedorCommandValidator : AbstractValidator<CreateProveedorCommand>
    {
        public CreateProveedorCommandValidator()
        {
            RuleFor(p => p.Nombre)
                .NotEmpty()
                .WithMessage("El nombre del proveedor no puede estar vacio");
        }
    }
}
