using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Application.Exceptions;
using AppAcademy.Domain.ControlAcademia;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Careers.Commands.UpdateCareer
{
    public class UpdateCareerCommandHandler : IRequestHandler<UpdateCareerCommand>
    {
        private readonly ICareerRepository _careerRepository;
        private readonly ILogger<UpdateCareerCommandHandler> _logger;

        public UpdateCareerCommandHandler(ICareerRepository careerRepository, ILogger<UpdateCareerCommandHandler> logger)
        {
            _careerRepository = careerRepository;
            _logger = logger;
        }

        public async Task Handle(UpdateCareerCommand request, CancellationToken cancellationToken)
        {
            var findCareer = await _careerRepository.GetByIdInt(request.CareerId);

            if (findCareer == null)
            {
                _logger.LogError($"No se encontro el id del career {request.CareerId}");
                throw new NotFoundException(nameof(Career), request.CareerId);
            }

            findCareer.Nombre = request.Nombre;
            findCareer.DuracionSemestres = request.DuracionSemestres;
            findCareer.CostoMensual = request.CostoMensual;
            findCareer.Activa = request.Activa;

            await _careerRepository.UpdateAsync(findCareer);

            _logger.LogInformation($"La operacion fue exitosa {request.CareerId}");
        }
    }
}
