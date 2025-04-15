using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Students.Queries.GetGanttData
{
    public class GetGanttDataListQueryHandler : IRequestHandler<GetGanttDataListQuery, List<GetStudentCardVm>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ILogger<GetGanttDataListQueryHandler> _logger;

        public GetGanttDataListQueryHandler(IStudentRepository studentRepository, ILogger<GetGanttDataListQueryHandler> logger)
        {
            _studentRepository = studentRepository;
            _logger = logger;
        }

        public async Task<List<GetStudentCardVm>> Handle(GetGanttDataListQuery request, CancellationToken cancellationToken)
        {
            var gants = await _studentRepository.GetStudentCard();

            return gants;
        }
    }
}
