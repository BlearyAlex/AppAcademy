using AppAcademy.Domain.PuntoDeVenta;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Marcas.Command.CreateMarca
{
    public class CreateMarcaCommand : IRequest<string>
    {
        public string? Nombre { get; set; }

        // Relaciones
        public List<Producto> Productos { get; set; } = [];
    }
}
