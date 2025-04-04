using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Abonos.Command.DeleteAbono
{
    public class DeleteAbonoCommand : IRequest<string>
    {
        public int AbonoId { get; set; }

        public string UserName { get; set; } = string.Empty;
    }
}
