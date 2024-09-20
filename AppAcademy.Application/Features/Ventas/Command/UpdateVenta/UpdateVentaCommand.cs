using AppAcademy.Domain.Auth;
using AppAcademy.Domain.PuntoDeVenta;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Ventas.Command.UpdateVenta
{
    public class UpdateVentaCommand : IRequest
    {
        public string ventaId { get; set; }
        public DateTime FechaCompra { get; set; }

        // Relaciones
        public string? ClienteId { get; set; }
        public string? UserId { get; set; }
    }
}
