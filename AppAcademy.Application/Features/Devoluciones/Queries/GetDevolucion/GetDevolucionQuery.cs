using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Devoluciones.Queries.GetDevolucion
{
    public class GetDevolucionQuery : IRequest<GetDevolucionVm>
    {
        public string _DevolucionId { get; set; }

        public GetDevolucionQuery(string devolucionId)
        {
            _DevolucionId = devolucionId;
        }
    }
}
