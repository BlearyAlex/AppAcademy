using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AutoMapper;
using MediatR;

namespace AppAcademy.Application.Features.Estudiantes.Queries.GetEstudianteById
{
    public class GetEstudianteByIdQueryHandler : IRequestHandler<GetEstudianteByIdQuery, GetEstudianteByIdVm>
    {
        private readonly IEstudianteRepository _estudianteRepository;
        private readonly IMapper _mapper;

        public GetEstudianteByIdQueryHandler(IEstudianteRepository estudianteRepository, IMapper mapper)
        {
            _estudianteRepository = estudianteRepository;
            _mapper = mapper;
        }

        public async Task<GetEstudianteByIdVm> Handle(GetEstudianteByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _estudianteRepository.GetById(request.EstudianteId);

            return _mapper.Map<GetEstudianteByIdVm>(student);
        }
    }
}
