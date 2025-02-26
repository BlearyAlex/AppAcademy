using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.StudentPermissions.Commands.DeleteStudentPermission
{
    public class DeleteStudentPermissionCommand : IRequest
    {
        public int StudentPermissionId { get; set; }
    }
}
