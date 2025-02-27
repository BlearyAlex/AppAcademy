using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Application.Exceptions;
using AppAcademy.Domain.PuntoDeVenta;
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

            // Si hay una imagen asociada, eliminarla del servidor
            if (!string.IsNullOrEmpty(findStudent.ImageUrl))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "students", "images", Path.GetFileName(findStudent.ImageUrl.TrimStart('/')));

                if (File.Exists(imagePath))
                {
                    try
                    {
                        File.Delete(imagePath);
                        _logger.LogInformation($"Imagen eliminada del servidor: {imagePath}");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Error al eliminar la imagen: {ex.Message}");
                        // Puedes optar por continuar o lanzar un error dependiendo de si quieres que falle la eliminación del producto si no se puede eliminar la imagen.
                    }
                }
                else
                {
                    _logger.LogWarning($"No se encontró la imagen en el servidor para eliminar: {imagePath}");
                }
            }

            await _studentRepository.DeleteAsync(findStudent);

            return;
        }
    }
}
