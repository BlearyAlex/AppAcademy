using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Colegiaturas.Queries.GetColegiatura
{
    public class GetColegiaturaQueryHandler : IRequestHandler<GetColegiaturaQuery, GetColegiaturaVm>
    {
        private readonly IColegiaturaRepository _colegiaturaRepository;
        private readonly IMapper _mapper;

        public GetColegiaturaQueryHandler(IColegiaturaRepository colegiaturaRepository, IMapper mapper)
        {
            _colegiaturaRepository = colegiaturaRepository;
            _mapper = mapper;
        }

        public async Task<GetColegiaturaVm> Handle(GetColegiaturaQuery request, CancellationToken cancellationToken)
        {
            var findColegiatura = await _colegiaturaRepository.GetById(request.ColegiaturaId);

            return _mapper.Map<GetColegiaturaVm>(findColegiatura);
        }
    }
}
