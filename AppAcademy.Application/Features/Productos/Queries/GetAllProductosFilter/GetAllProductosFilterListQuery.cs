using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Productos.Queries.GetAllProductosFilter
{
    public class GetAllProductosFilterListQuery : IRequest<List<GetAllProductosFilterVm>>
    {
    }
}
