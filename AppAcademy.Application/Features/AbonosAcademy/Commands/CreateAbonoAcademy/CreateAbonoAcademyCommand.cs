using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.AbonosAcademy.Commands.CreateAbonoAcademy
{
    public class CreateAbonoAcademyCommand : IRequest<bool>
    {
        public int PaymentId { get; set; }
        public decimal MontoAbonado { get; set; }
    }
}
