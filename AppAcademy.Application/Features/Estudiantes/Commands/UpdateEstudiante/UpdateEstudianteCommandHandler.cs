using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Application.Exceptions;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Estudiantes.Commands.UpdateEstudiante
{
    public class UpdateEstudianteCommandHandler : IRequestHandler<UpdateEstudianteCommand>
    {
        private readonly IEstudianteRepository _estudianteRepository;
        private readonly IMapper _mapper;

        public UpdateEstudianteCommandHandler(IEstudianteRepository estudianteRepository, IMapper mapper)
        {
            _estudianteRepository = estudianteRepository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateEstudianteCommand request, CancellationToken cancellationToken)
        {
            var findStudent = await _estudianteRepository.GetById(request.EstudianteId);

            if (findStudent == null)
            {
                throw new NotFoundException(nameof(findStudent), request.EstudianteId);
            }

            _mapper.Map(request, findStudent);

            await _estudianteRepository.UpdateAsync(findStudent);
        }
    }
}
