using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.EntradasProductos.Queries.GetAllEntradas
{
    public class GetAllEntradasProductosListQuery : IRequest<List<GetAllEntradasProductosVm>>
    {
    }
}
