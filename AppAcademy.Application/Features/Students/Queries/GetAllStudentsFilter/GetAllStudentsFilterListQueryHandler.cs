using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Students.Queries.GetAllStudentsFilter
{
    public class GetAllStudentsFilterListQueryHandler : IRequestHandler<GetAllStudentsFilterListQuery, List<GetAllStudentsFilterVm>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ILogger<GetAllStudentsFilterListQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllStudentsFilterListQueryHandler(IStudentRepository studentRepository, ILogger<GetAllStudentsFilterListQueryHandler> logger, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<GetAllStudentsFilterVm>> Handle(GetAllStudentsFilterListQuery request, CancellationToken cancellationToken)
        {
            var studentList = await _studentRepository.GetAllStudentsFiltersWithCareers();

            return _mapper.Map<List<GetAllStudentsFilterVm>>(studentList);
        }
    }
}
