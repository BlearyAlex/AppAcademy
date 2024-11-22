using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Ventas.Queries.GetVentasForDate
{
    public class GetVentasForDateQuery : IRequest<List<GetVentasForDateVm>>
    {
        public string Periodo { get; set; }

        public GetVentasForDateQuery(string periodo)
        {
            Periodo = periodo;
        }
    }
}
