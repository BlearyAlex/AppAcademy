using AppAcademy.Application.Contracts.Persistence;
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
        private readonly IFileStorageService _fileStorageService;

        public UpdateStudentCommandHandler(IStudentRepository studentRepository, ILogger<UpdateStudentCommandHandler> logger, IFileStorageService fileStorageService)
        {
            _studentRepository = studentRepository;
            _logger = logger;
            _fileStorageService = fileStorageService;
        }

        public async Task Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var findStudent = await _studentRepository.GetByIdInt(request.StudentId);

            if (findStudent == null)
            {
                _logger.LogError($"No se encontro el id del student {request.StudentId}");
                throw new NotFoundException(nameof(Student), request.StudentId);
            }

            // Si se proporciona una nueva imagen
            if (request.ImageFile != null && request.ImageFile.Length > 0)
            {
                try
                {
                    // Guardar la nueva imagen y obtener la URL
                    var imageUrl = await _fileStorageService.SaveImageAndGetUrl(request.ImageFile);
                    findStudent.ImageUrl= imageUrl;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error al procesar la nueva imagen: {ex.Message}");
                    throw new ApplicationException("No se pudo guardar la nueva imagen del estudiante");
                }
            }

            findStudent.Nombre = request.Nombre;
            findStudent.Apellido = request.Apellido;
            findStudent.Telefono = request.Telefono;
            findStudent.Email = request.Email;
            findStudent.Direccion = request.Direccion;
            findStudent.EstadoEstudiante = request.EstadoEstudiante;
            findStudent.CareerId = request.CareerId;
            findStudent.AcademicCycleId = request.AcademicCycleId; 

            await _studentRepository.UpdateAsync(findStudent);

            _logger.LogInformation($"La operacion fue exitosa {request.StudentId}");
        }
    }
}
