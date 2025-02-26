using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Application.Exceptions;
using AppAcademy.Domain.ControlAcademia;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.AcademicCycles.Commands.DeleteCycle
{
    public class DeleteCycleCommandHandler : IRequestHandler<DeleteCycleCommand>
    {
        private readonly IAcademicCycleRepository _academicCycleRepository;
        private readonly ILogger<DeleteCycleCommandHandler> _logger;

        public DeleteCycleCommandHandler(IAcademicCycleRepository academicCycleRepository, ILogger<DeleteCycleCommandHandler> logger)
        {
            _academicCycleRepository = academicCycleRepository;
            _logger = logger;
        }

        public async Task Handle(DeleteCycleCommand request, CancellationToken cancellationToken)
        {
            var findCycle = await _academicCycleRepository.GetByIdInt(request.AcademicCycleId);

            if (findCycle == null)
            {
                _logger.LogError($"{request.AcademicCycleId} ciclo academico no existe en el sistema");
                throw new NotFoundException(nameof(AcademicCycle), request.AcademicCycleId);
            }

            await _academicCycleRepository.DeleteAsync(findCycle);

            return; 
        }
    }
}
