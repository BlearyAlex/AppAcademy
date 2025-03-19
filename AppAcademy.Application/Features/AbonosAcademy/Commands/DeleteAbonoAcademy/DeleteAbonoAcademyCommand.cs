using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.AbonosAcademy.Commands.DeleteAbonoAcademy
{
    public class DeleteAbonoAcademyCommand : IRequest<bool>
    {
        public int AbonoAcademyId { get; set; }

        public string UserName { get; set; } = string.Empty;
    }
}
