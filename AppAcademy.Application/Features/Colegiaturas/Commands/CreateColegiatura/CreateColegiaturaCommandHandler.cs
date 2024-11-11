using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Domain.ControlAcademia;
using AutoMapper;
using MediatR;

namespace AppAcademy.Application.Features.Colegiaturas.Commands.CreateColegiatura
{
    public class CreateColegiaturaCommandHandler : IRequestHandler<CreateColegiaturaCommand, string>
    {
        private readonly IMapper _mapper;
        private readonly IColegiaturaRepository _colegiaturaRepository;

        public CreateColegiaturaCommandHandler(IMapper mapper, IColegiaturaRepository colegiaturaRepository)
        {
            _mapper = mapper;
            _colegiaturaRepository = colegiaturaRepository;
        }

        public async Task<string> Handle(CreateColegiaturaCommand request, CancellationToken cancellationToken)
        {
            var colegiatura = _mapper.Map<Colegiatura>(request);
            colegiatura.Anio = DateTime.Now.Year;
            colegiatura.FechaPago = DateTime.Now;

            var newColegiatura = await _colegiaturaRepository.AddAsync(colegiatura);

            return newColegiatura.ColegiaturaId;
        }
    }
}
