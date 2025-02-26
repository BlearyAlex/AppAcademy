using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.StudentPermissions.Queries.GetStudentPermission
{
    public class GetStudentPermissionQuery : IRequest<GetStudentPermissionVm>
    {
        public int _StudentPermissionId { get; set; }

        public GetStudentPermissionQuery(int studentPermissionId)
        {
            _StudentPermissionId = studentPermissionId;
        }
    }
}
