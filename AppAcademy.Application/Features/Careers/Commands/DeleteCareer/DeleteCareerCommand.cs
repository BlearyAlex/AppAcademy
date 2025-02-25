using MediatR;

namespace AppAcademy.Application.Features.Careers.Commands.DeleteCareer
{
    public class DeleteCareerCommand : IRequest
    {
        public int CareerId { get; set; }
    }
}
