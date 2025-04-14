using AppAcademy.Application.Contracts.Persistence;
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
        private readonly IFileStorageService _fileStorageService;

        public CreateStudentCommandHandler(IStudentRepository studentRepository, ILogger<CreateStudentCommandHandler> logger, IFileStorageService fileStorageService)
        {
            _studentRepository = studentRepository;
            _logger = logger;
            _fileStorageService = fileStorageService;
        }

        public async Task<int> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            return await _studentRepository.CreateStudent(request, request.UserName);
        }
    }
}
