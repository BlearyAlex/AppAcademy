using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Ventas.Command.DeleteVenta
{
    public class DeleteVentaCommand : IRequest
    {
        public string VentaId { get; set; }
    }
}
