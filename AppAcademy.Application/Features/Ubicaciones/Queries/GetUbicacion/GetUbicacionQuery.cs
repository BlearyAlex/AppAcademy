using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Ubicaciones.Queries.GetUbicacion
{
    public class GetUbicacionQuery : IRequest<GetUbicacionVm>
    {
        public string _UbicacionId { get; set; }

        public GetUbicacionQuery(string ubicacion)
        {
            _UbicacionId = ubicacion;
        }
    }
}
