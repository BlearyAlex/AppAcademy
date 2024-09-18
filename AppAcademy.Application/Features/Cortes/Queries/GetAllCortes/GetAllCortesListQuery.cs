using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Cortes.Queries.GetAllCortes
{
    public class GetAllCortesListQuery : IRequest<List<GetAllCortesVm>>
    {
    }
}
