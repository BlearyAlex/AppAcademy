using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.AbonosAcademy.Commands.DeleteAbonoAcademy
{
    public class DeleteAbonoAcademyCommandHandler : IRequestHandler<DeleteAbonoAcademyCommand, bool>
    {
        private readonly IAbonoAcademyRepository _abonoAcademyRepository;

        public DeleteAbonoAcademyCommandHandler(IAbonoAcademyRepository abonoAcademyRepository)
        {
            _abonoAcademyRepository = abonoAcademyRepository;
        }

        public async Task<bool> Handle(DeleteAbonoAcademyCommand request, CancellationToken cancellationToken)
        {
            return await _abonoAcademyRepository.DeleteAbono(request.AbonoAcademyId, request.UserName);  
        }
    }
}
