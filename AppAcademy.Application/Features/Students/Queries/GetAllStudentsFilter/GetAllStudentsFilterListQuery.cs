using MediatR;

namespace AppAcademy.Application.Features.Students.Queries.GetAllStudentsFilter
{
    public class GetAllStudentsFilterListQuery : IRequest<List<GetAllStudentsFilterVm>>
    {
    }
}
