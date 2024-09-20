using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Devoluciones.Queries.GetAllDevoluciones
{
    public class GetAllDevolucionesListQuery : IRequest<List<GetAllDevolucionesVm>>
    {
    }
}
