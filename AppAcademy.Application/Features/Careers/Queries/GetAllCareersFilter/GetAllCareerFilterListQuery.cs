using MediatR;

namespace AppAcademy.Application.Features.Careers.Queries.GetAllCareersFilter
{
    public class GetAllCareerFilterListQuery : IRequest<List<GetAllCareerFilterVm>>
    {
    }
}
