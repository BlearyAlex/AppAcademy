using MediatR;

namespace AppAcademy.Application.Features.Students.Queries.GetStudent
{
    public class GetStudentQuery : IRequest<GetStudentVm>
    {
        public int _StudentId { get; set; }

        public GetStudentQuery(int studentId)
        {
            _StudentId = studentId;
        }
    }
}
