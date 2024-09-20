using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Ubicaciones.Queries.GetAllUbicaciones
{
    public class GetAllUbicacionesListQuery : IRequest<List<GetAllUbicacionesVm>>
    {
    }
}
