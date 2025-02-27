using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Domain.ControlAcademia;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Students.Commands.CreateStudent
{
    public class CreateCareerCommandHandler : IRequestHandler<CreateStudentCommand, int>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ILogger<CreateCareerCommandHandler> _logger;

        public CreateCareerCommandHandler(IStudentRepository studentRepository, ILogger<CreateCareerCommandHandler> logger)
        {
            _studentRepository = studentRepository;
            _logger = logger;
        }

        public async Task<int> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            if (request.ImageFile != null && request.ImageFile.Length > 0)
            {
                try
                {
                    // Guardar la imagen y obtener la url
                    var imageUrl = await SaveImageAndGetUrl(request.ImageFile);
                    request.ImageUrl = imageUrl; // Almacenar la Url de la imagen en el modelo 
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error al procesar la imagen: {ex.Message}");
                    throw new ApplicationException("No se pudo guardar la imagen del estudiante");
                }
            }

            var student = new Student
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Telefono = request.Telefono,
                Email = request.Email,
                Direccion = request.Direccion,
                ImageUrl = request.ImageUrl,
                FechaIngreso = DateTime.Now,
                EstadoEstudiante = request.EstadoEstudiante,
                CareerId = request.CareerId,
            };

            var newStudent = await _studentRepository.AddAsync(student);

            return newStudent.StudentId;
        }

        private async Task<string> SaveImageAndGetUrl(IFormFile imageFile)
        {
            try
            {
                // Generar un nombre único para la imagen
                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(imageFile.FileName)}";

                // Crear directorio si no existe
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var filePath = Path.Combine(directoryPath, fileName);

                // Guardar la imagen en el servidor
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }

                // Devolver la URL de la imagen
                return $"/images/{fileName}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al guardar la imagen: {ex.Message}");
                throw new ApplicationException("Error al guardar la imagen del producto");
            }
        }

    }
}
