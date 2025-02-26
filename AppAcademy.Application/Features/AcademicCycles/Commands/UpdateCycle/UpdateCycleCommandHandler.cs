using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Application.Exceptions;
using AppAcademy.Domain.ControlAcademia;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.AcademicCycles.Commands.UpdateCycle
{
    public class UpdateCycleCommandHandler : IRequestHandler<UpdateCycleCommand, int>
    {
        private readonly IAcademicCycleRepository _academicCycleRepository;
        private readonly ILogger<UpdateCycleCommandHandler> _logger;

        public UpdateCycleCommandHandler(IAcademicCycleRepository academicCycleRepository, ILogger<UpdateCycleCommandHandler> logger)
        {
            _academicCycleRepository = academicCycleRepository;
            _logger = logger;
        }

        public async Task<int> Handle(UpdateCycleCommand request, CancellationToken cancellationToken)
        {
            var findCycle = await _academicCycleRepository.GetByIdInt(request.AcademicCycleId);

            if (findCycle == null)
            {
                _logger.LogError($"No se encontro el id del ciclo escolar {request.AcademicCycleId}");
                throw new NotFoundException(nameof(AcademicCycle), request.AcademicCycleId);
            }

            findCycle.NumeroCiclo = request.NumeroCiclo;
            findCycle.FechaInicio = request.FechaInicio;
            findCycle.FechaFin = request.FechaFin;
            findCycle.CareerId = request.CareerId;

            await _academicCycleRepository.UpdateAsync(findCycle);

            return findCycle.AcademicCycleId;
        }
    }
}
