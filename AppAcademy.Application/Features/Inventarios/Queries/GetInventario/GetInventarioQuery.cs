using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Inventarios.Queries.GetInventario
{
    public class GetInventarioQuery : IRequest<GetInventarioVm>
    {
        public string _InventarioId { get; set; }
    }
}
