using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Domain.ControlAcademia;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.AcademicCycles.Commands.CreateCycle
{
    public class CreateCycleCommandHandler : IRequestHandler<CreateCycleCommand, int>
    {
        private readonly IAcademicCycleRepository _academicCycleRepository;
        private readonly ILogger<CreateCycleCommandHandler> _logger;

        public CreateCycleCommandHandler(IAcademicCycleRepository academicCycleRepository, ILogger<CreateCycleCommandHandler> logger)
        {
            _academicCycleRepository = academicCycleRepository;
            _logger = logger;
        }

        public async Task<int> Handle(CreateCycleCommand request, CancellationToken cancellationToken)
        {
            var cycle = new AcademicCycle
            {
                CicloAcademico = request.CicloAcademico,
                FechaInicio = request.FechaInicio,
                Color = request.Color,
                FechaFin = request.FechaFin,
                CareerId = request.CareerId,
            };

            var newCycle = await _academicCycleRepository.AddAsync(cycle);

            return newCycle.AcademicCycleId;
        }
    }
}
