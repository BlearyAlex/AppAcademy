using MediatR;

namespace AppAcademy.Application.Features.Careers.Queries.GetCareer
{
    public class GetCareerQuery : IRequest<GetCareerVm>
    {
        public int _CareerId { get; set; }

        public GetCareerQuery(int careerId)
        {
            _CareerId = careerId;
        }
    }
}
