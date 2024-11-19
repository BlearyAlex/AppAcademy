using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Estudiantes.Commands.UpdateEstudiante
{
    public class UpdateEstudianteCommandHandler : IRequestHandler<UpdateEstudianteCommand>
    {
        private readonly IEstudianteRepository _estudianteRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateEstudianteCommandHandler> _logger;

        public UpdateEstudianteCommandHandler(IEstudianteRepository estudianteRepository, IMapper mapper, ILogger<UpdateEstudianteCommandHandler> logger)
        {
            _estudianteRepository = estudianteRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(UpdateEstudianteCommand request, CancellationToken cancellationToken)
        {
            var student = await _estudianteRepository.GetById(request.EstudianteId);

            if (student == null)
            {
                _logger.LogError($"Estudiante con ID {request.EstudianteId} no encontrado.");
                throw new ApplicationException("Estudiante no encontrado");
            }

            // Si se proprciona una nueva imagen
            if (request.ImageFile != null && request.ImageFile.Length > 0)
            {
                try
                {
                    // Guardar la imagen y obtener la url
                    var imageUrl = await SaveImageAndGetUrl(request.ImageFile);
                    request.ImageUrl = imageUrl;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error al procesar la nueva imagen: {ex.Message}");
                    throw new ApplicationException("No se pudo guardar la nueva imagen del producto");
                }
            }
            else
            {
                request.ImageUrl = student.ImageUrl;
            }

            // Mapear los datos actualizados del comando al modelo de producto
            _mapper.Map(request, student);

            await _estudianteRepository.UpdateAsync(student);

            _logger.LogInformation($"Estudiante {student.EstudianteId} actualizado exitosamente");
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
