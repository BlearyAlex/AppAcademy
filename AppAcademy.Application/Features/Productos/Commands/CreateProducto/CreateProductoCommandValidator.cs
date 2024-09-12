using FluentValidation;

namespace AppAcademy.Application.Features.Productos.Commands.CreateProducto
{
    public class CreateProductoCommandValidator : AbstractValidator<CreateProductoCommand>
    {
        public CreateProductoCommandValidator()
        {
            RuleFor(p => p.Nombre)
               .NotEmpty()
               .WithMessage("El nombre del producto no puede estar vacío.")
               .MaximumLength(50)
               .WithMessage("El nombre del producto no puede exceder los 50 caracteres.");

            RuleFor(p => p.Costo)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El Costo no puede ser menor que 0.");

            RuleFor(p => p.Precio)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El Precio no puede ser menor que 0.");

            RuleFor(p => p.DescuentoBase)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El Descuento Base no puede ser menor que 0.");

            RuleFor(p => p.Impuesto)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El Impuesto no puede ser menor que 0.");

            RuleFor(p => p.EstadoProducto)
                .IsInEnum()
                .WithMessage("El Estado del producto no es válido.");

            RuleFor(p => p.StockMinimo)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El Stock Mínimo no puede ser menor que 0.");

            RuleFor(p => p.StockMaximo)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El Stock Máximo no puede ser menor que 0.");
        }
    }
}
