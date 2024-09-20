using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Entradas.Queries.GetEntrada
{
    public class GetEntradaVm
    {
        public int TotalProductosEntrada { get; set; }
        public DateTime FechaDeEntrega { get; set; }
        public string? NumeroFactura { get; set; }
        public int VencimientoPago { get; set; }
        public string? Folio { get; set; }
        public decimal Bruto { get; set; }

        // Relaciones
        public string? UserId { get; set; }

        public string? OrigenId { get; set; }
    }
}
