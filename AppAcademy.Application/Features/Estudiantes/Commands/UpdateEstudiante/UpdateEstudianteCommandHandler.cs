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
        private readonly IEstudianteRepository _esteudianteRepository;
        private readonly IMapper _mapper;

        public UpdateEstudianteCommandHandler(IEstudianteRepository esteudianteRepository, IMapper mapper)
        {
            _esteudianteRepository = esteudianteRepository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateEstudianteCommand request, CancellationToken cancellationToken)
        {
            var findStudent = await _esteudianteRepository.GetById(request.EstudianteId);

            if (findStudent == null)
            {
                throw new NotFoundException(nameof(findStudent), request.EstudianteId);
            }

            _mapper.Map(request, findStudent);

            await _esteudianteRepository.UpdateAsync(findStudent);
        }
    }
}
