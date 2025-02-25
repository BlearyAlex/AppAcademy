using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Careers.Commands.DeleteCareer
{
    public class DeleteCareerCommandHandler : IRequestHandler<DeleteCareerCommand>
    {
        private readonly ICareerRepository _careerRepository;
        private readonly ILogger<DeleteCareerCommandHandler> _logger;

        public DeleteCareerCommandHandler(ICareerRepository careerRepository, ILogger<DeleteCareerCommandHandler> logger)
        {
            _careerRepository = careerRepository;
            _logger = logger;
        }

        public async Task Handle(DeleteCareerCommand request, CancellationToken cancellationToken)
        {
            var findCareer = await _careerRepository.GetByIdInt(request.CareerId);
            if (findCareer == null)
            {
                _logger.LogError($"{request.CareerId} career no existe en el sistema");
                throw new NotFoundException(nameof(findCareer), request.CareerId);
            }

            await _careerRepository.DeleteAsync(findCareer);

            return;
        }
    }
}
