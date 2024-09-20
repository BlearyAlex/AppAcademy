using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Marcas.Queries.GetAllMarcas
{
    public class GetAllMarcasListQuery : IRequest<List<GetAllMarcasVm>>
    {
    }
}
