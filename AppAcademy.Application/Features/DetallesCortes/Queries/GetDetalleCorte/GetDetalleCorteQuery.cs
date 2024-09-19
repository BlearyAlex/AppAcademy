using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.DetallesCortes.Queries.GetDetalleCorte
{
    public class GetDetalleCorteQuery : IRequest<GetDetalleCorteVm>
    {
        public string _DetalleCorteId { get; set; }

        public GetDetalleCorteQuery(string detalleCorteId)
        {
            _DetalleCorteId = detalleCorteId;
        }
    }
}
