using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Application.Exceptions;
using MediatR;

namespace AppAcademy.Application.Features.Estudiantes.Commands.DeleteEstudiante
{
    public class DeleteEstudianteCommandHandler : IRequestHandler<DeleteEstudianteCommand>
    {
        private readonly IEstudianteRepository _estudianteRepository;

        public DeleteEstudianteCommandHandler(IEstudianteRepository estudianteRepository)
        {
            _estudianteRepository = estudianteRepository;
        }

        public async Task Handle(DeleteEstudianteCommand request, CancellationToken cancellationToken)
        {
            var foundStudent = await _estudianteRepository.GetById(request.EstudianteId);
            if (foundStudent == null)
            {
                throw new NotFoundException(nameof(foundStudent), request.EstudianteId);
            }

            await _estudianteRepository.DeleteAsync(foundStudent);

            return;
        }
    }
}
