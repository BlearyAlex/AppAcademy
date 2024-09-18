using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Cortes.Queries.GetCorte
{
    public class GetCorteQuery : IRequest<GetCorteVm>
    {
        public string _CorteId { get; set; }

        public GetCorteQuery(string corteId)
        {
            _CorteId = corteId;
        }
    }
}
