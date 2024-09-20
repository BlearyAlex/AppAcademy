using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Devoluciones.Command.DeleteDevolucion
{
    public class DeleteDevolucionCommand : IRequest
    {
        public string DevolucionId { get; set; }
    }
}
