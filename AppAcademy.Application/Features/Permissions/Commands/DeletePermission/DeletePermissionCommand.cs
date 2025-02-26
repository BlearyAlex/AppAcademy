using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Permissions.Commands.DeletePermission
{
    public class DeletePermissionCommand : IRequest
    {
        public int PermissionId { get; set; }
    }
}
