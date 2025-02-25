using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Application.Exceptions;
using AppAcademy.Domain.ControlAcademia;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Students.Commands.UpdateStudent
{
    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ILogger<UpdateStudentCommandHandler> _logger;

        public UpdateStudentCommandHandler(IStudentRepository studentRepository, ILogger<UpdateStudentCommandHandler> logger)
        {
            _studentRepository = studentRepository;
            _logger = logger;
        }

        public async Task Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var findStudent = await _studentRepository.GetByIdInt(request.StudentId);

            if (findStudent == null)
            {
                _logger.LogError($"No se encontro el id del student {request.StudentId}");
                throw new NotFoundException(nameof(Student), request.StudentId);
            }

            findStudent.Nombre = request.Nombre;
            findStudent.Apellido = request.Apellido;
            findStudent.Telefono = request.Telefono;
            findStudent.Email = request.Email;
            findStudent.Direccion = request.Direccion;
            findStudent.ImageUrl = request.ImageUrl;
            findStudent.EstadoEstudiante = request.EstadoEstudiante;
            findStudent.CareerId = request.CareerId;

            await _studentRepository.UpdateAsync(findStudent);

            _logger.LogInformation($"La operacion fue exitosa {request.StudentId}");
        }
    }
}
