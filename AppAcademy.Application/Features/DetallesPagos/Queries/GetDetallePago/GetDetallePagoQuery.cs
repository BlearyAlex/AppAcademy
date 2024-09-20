using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.DetallesPagos.Queries.GetDetallePago
{
    public class GetDetallePagoQuery : IRequest<GetDetallePagoVm>
    {
        public string _DetallePagoId { get; set; }

        public GetDetallePagoQuery(string detallePagoId)
        {
            _DetallePagoId = detallePagoId;
        }
    }
}
