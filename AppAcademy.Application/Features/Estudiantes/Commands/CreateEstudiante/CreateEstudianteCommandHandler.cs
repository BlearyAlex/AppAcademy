using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Domain.ControlAcademia;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            try
            {
                var newStudent = _mapper.Map<Estudiante>(request);

                var addStudent = await _estudianteRepository.AddAsync(newStudent);

                return addStudent.EstudianteId;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
