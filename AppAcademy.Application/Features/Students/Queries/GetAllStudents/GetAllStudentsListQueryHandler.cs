using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Students.Queries.GetAllStudents
{
    public class GetAllStudentsListQueryHandler : IRequestHandler<GetAllStudentsListQuery, List<GetAllStudentsVm>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ILogger<GetAllStudentsListQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllStudentsListQueryHandler(IStudentRepository studentRepository, ILogger<GetAllStudentsListQueryHandler> logger, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<GetAllStudentsVm>> Handle(GetAllStudentsListQuery request, CancellationToken cancellationToken)
        {
            var studentsList = await _studentRepository.GetAllStudentsWithCareers();

            return _mapper.Map<List<GetAllStudentsVm>>(studentsList);
        }
    }
}
