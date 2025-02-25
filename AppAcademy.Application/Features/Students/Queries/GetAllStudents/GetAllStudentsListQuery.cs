using MediatR;

namespace AppAcademy.Application.Features.Students.Queries.GetAllStudents
{
    public class GetAllStudentsListQuery : IRequest<List<GetAllStudentsVm>>
    {
    }
}
