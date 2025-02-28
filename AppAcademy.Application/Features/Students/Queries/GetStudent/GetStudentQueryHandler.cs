using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AutoMapper;
using MediatR;

namespace AppAcademy.Application.Features.Students.Queries.GetStudent
{
    public class GetStudentQueryHandler : IRequestHandler<GetStudentQuery, GetStudentVm>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public GetStudentQueryHandler(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<GetStudentVm> Handle(GetStudentQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetStudentsByIdWithCareer(request._StudentId);

            return _mapper.Map<GetStudentVm>(student);
        }
    }
}
