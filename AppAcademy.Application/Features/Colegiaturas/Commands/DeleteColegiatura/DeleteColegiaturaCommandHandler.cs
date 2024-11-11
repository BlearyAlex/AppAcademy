using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Colegiaturas.Commands.DeleteColegiatura
{
    public class DeleteColegiaturaCommandHandler : IRequestHandler<DeleteColegiaturaCommand>
    {
        private readonly IColegiaturaRepository _colegiaturaRepository;

        public DeleteColegiaturaCommandHandler(IColegiaturaRepository colegiaturaRepository)
        {
            _colegiaturaRepository = colegiaturaRepository;
        }

        public async Task Handle(DeleteColegiaturaCommand request, CancellationToken cancellationToken)
        {
            var findColegiatura = await _colegiaturaRepository.GetById(request.ColegiaturaId);
            if (findColegiatura == null)
            {
                throw new NotFoundException(nameof(findColegiatura), request.ColegiaturaId);
            }

            var deletColegiatura = await _colegiaturaRepository.DeleteAsync(findColegiatura);
        }
    }
}
