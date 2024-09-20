using AppAcademy.Domain.Auth;
using AppAcademy.Domain.PuntoDeVenta;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Entradas.Command.CreateEntrada
{
    public class CreateEntradaCommand : IRequest<string>
    {
        public int TotalProductosEntrada { get; set; }
        public DateTime FechaDeEntrega { get; set; }
        public string? NumeroFactura { get; set; }
        public int VencimientoPago { get; set; }
        public string? Folio { get; set; }
        public decimal Bruto { get; set; }

        public string? UserId { get; set; }

        public string? OrigenId { get; set; }
    }
}
