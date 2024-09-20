using AppAcademy.Domain.Auth;
using AppAcademy.Domain.PuntoDeVenta;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.HistorialInventarios.Command.CreateHistorialInventario
{
    public class CreateHistorialInventarioCommand : IRequest<string>
    {
        public int CantidadAnterior { get; set; }
        public int CantidadNueva { get; set; }
        public DateTime FechaCambio { get; set; }
        public string? Motivo { get; set; }

        // Relaciones
        public string? UserId { get; set; }
        public string? ProductoId { get; set; }
    }
}
