using AppAcademy.Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.DetallesPagos.Command.UpdateDetallePago
{
    public class UpdateDetallePagoCommand : IRequest
    {
        public string DetallePagoId { get; set; }
        public TipoPago Tipo { get; set; }
        public decimal Monto { get; set; }

        public string? VentaId { get; set; }
    }
}
