using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using MediatR;
using AppAcademy.Domain.ControlAcademia;

namespace AppAcademy.Application.Features.Careers.Commands.CreateCareer
{
    public class CreateCareerCommandHandler : IRequestHandler<CreateCareerCommand, int>
    {
        private readonly ICareerRepository _careerRepository;

        public CreateCareerCommandHandler(ICareerRepository careerRepository)
        {
            _careerRepository = careerRepository;
        }

        public async Task<int> Handle(CreateCareerCommand request, CancellationToken cancellationToken)
        {
            var career = new Career
            {
                Nombre = request.Nombre,
                DuracionSemestres = request.DuracionSemestres,
                CostoMensual = request.CostoMensual,
                Activa = request.Activa,
            };

            var newCareer = await _careerRepository.AddAsync(career);

            return newCareer.CareerId;
        }
    }
}
