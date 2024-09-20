using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.HistorialInventarios.Queries.GetHistorialInventario
{
    public class GetHistorialInventarioQuery : IRequest<GetHistorialInventarioVm>
    {
        public string _HistorialInventarioId { get; set; }
    }
}
