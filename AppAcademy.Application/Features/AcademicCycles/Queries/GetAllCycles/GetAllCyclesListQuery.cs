using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.AcademicCycles.Queries.GetAllCycles
{
    public class GetAllCyclesListQuery : IRequest<List<GetAllCyclesVm>>
    {
    }
}
