using AppAcademy.Domain.PuntoDeVenta;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Ubicaciones.Command.CreateUbicacion
{
    public class CreateUbicacionCommand : IRequest<string>
    {
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
    }
}
