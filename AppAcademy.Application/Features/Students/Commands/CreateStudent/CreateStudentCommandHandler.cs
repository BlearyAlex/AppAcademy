using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Domain.ControlAcademia;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Students.Commands.CreateStudent
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, int>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ILogger<CreateStudentCommandHandler> _logger;

        public CreateStudentCommandHandler(IStudentRepository studentRepository, ILogger<CreateStudentCommandHandler> logger)
        {
            _studentRepository = studentRepository;
            _logger = logger;
        }

        public async Task<int> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            return await _studentRepository.CreateStudent(request, request.UserName);
        }
    }
}
