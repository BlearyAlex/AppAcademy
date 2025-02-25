using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Students.Commands.DeleteStudent
{
    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ILogger<DeleteStudentCommandHandler> _logger;

        public DeleteStudentCommandHandler(IStudentRepository studentRepository, ILogger<DeleteStudentCommandHandler> logger)
        {
            _studentRepository = studentRepository;
            _logger = logger;
        }

        public async Task Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var findStudent = await _studentRepository.GetByIdInt(request.StudentId);
            if (findStudent == null)
            {
                _logger.LogError($"{request.StudentId} student no existe en el sistema");
                throw new NotFoundException(nameof(findStudent), request.StudentId);
            }

            await _studentRepository.DeleteAsync(findStudent);

            return;
        }
    }
}
