using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Domain.ControlAcademia;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Estudiantes.Commands.CreateEstudiante
{
    public class CreateEstudianteCommandHandler : IRequestHandler<CreateEstudianteCommand, string>
    {
        private readonly IEstudianteRepository _estudianteRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateEstudianteCommandHandler> _logger;

        public CreateEstudianteCommandHandler(IEstudianteRepository estudianteRepository, IMapper mapper, ILogger<CreateEstudianteCommandHandler> logger)
        {
            _estudianteRepository = estudianteRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> Handle(CreateEstudianteCommand request, CancellationToken cancellationToken)
        {
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
                    _logger.LogError($"Error al procesar la imagen: {ex.Message}");
                    throw new ApplicationException("No se pudo guardar la imagen del producto");
                }

            }

            var student = _mapper.Map<Estudiante>(request);

            var newStudent = await _estudianteRepository.AddAsync(student);

            return newStudent.EstudianteId;
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
