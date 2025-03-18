using MediatR;


namespace AppAcademy.Application.Features.Students.Commands.DeleteStudent
{
    public class DeleteStudentCommand : IRequest<bool>
    {
        public int StudentId { get; set; }

        public string UserName { get; set; } = string.Empty;
    }
}
