using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AutoMapper;
using MediatR;

namespace AppAcademy.Application.Features.Estudiantes.Queries.GetEstudianteWithColegiaturas
{
    public class GetEstudianteWithColegiaturaQueryHandler : IRequestHandler<GetEstudianteWithColegiaturaQuery, GetEstudianteWithColegiaturaVm>
    {
        private readonly IEstudianteRepository _estudianteRepository;
        private readonly IMapper _mapper;

        public GetEstudianteWithColegiaturaQueryHandler(IEstudianteRepository estudianteRepository, IMapper mapper)
        {
            _estudianteRepository = estudianteRepository;
            _mapper = mapper;
        }

        public async Task<GetEstudianteWithColegiaturaVm> Handle(GetEstudianteWithColegiaturaQuery request, CancellationToken cancellationToken)
        {
            var findStudent = await _estudianteRepository.GetEstudianteWithColegiatura(request.EstudianteId);

            return _mapper.Map<GetEstudianteWithColegiaturaVm>(findStudent);
        }
    }
}
