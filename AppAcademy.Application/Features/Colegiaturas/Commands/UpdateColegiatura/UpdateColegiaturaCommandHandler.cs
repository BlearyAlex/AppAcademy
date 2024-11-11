using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Colegiaturas.Commands.UpdateColegiatura
{
    public class UpdateColegiaturaCommandHandler : IRequestHandler<UpdateColegiaturaCommand>
    {
        private readonly IColegiaturaRepository _colegiaturaRepository;
        private readonly IMapper _mapper;

        public UpdateColegiaturaCommandHandler(IColegiaturaRepository colegiaturaRepository, IMapper mapper)
        {
            _colegiaturaRepository = colegiaturaRepository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateColegiaturaCommand request, CancellationToken cancellationToken)
        {
            var findColegiatura = await _colegiaturaRepository.GetById(request.ColegiaturaId);
            findColegiatura.Anio = DateTime.Now.Year;
            findColegiatura.FechaPago = DateTime.Now;

            _mapper.Map(request, findColegiatura);

            await _colegiaturaRepository.UpdateAsync(findColegiatura);
        }
    }
}
