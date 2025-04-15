using MediatR;

namespace AppAcademy.Application.Features.Careers.Queries.GetAllCareers
{
    public class GetAllCareerListQuery : IRequest<List<GetAllCareersVm>>
    {
    }
}
