using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Colegiaturas.Queries.GetColegiatura
{
    public class GetColegiaturaQuery : IRequest<GetColegiaturaVm>
    {
        public string ColegiaturaId { get; set; }

        public GetColegiaturaQuery(string colegiaturaId)
        {
            ColegiaturaId = colegiaturaId;
        }
    }
}
