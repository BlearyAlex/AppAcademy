using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Careers.Queries.GetAllCareers
{
    public class GetAllCareerListQuery : IRequest<List<GetAllCareersVm>>
    {
    }
}
