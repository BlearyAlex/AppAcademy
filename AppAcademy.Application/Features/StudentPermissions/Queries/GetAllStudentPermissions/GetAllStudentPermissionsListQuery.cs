using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.StudentPermissions.Queries.GetAllStudentPermissions
{
    public class GetAllStudentPermissionsListQuery : IRequest<List<GetAllStudentPermissionsVm>>
    {
    }
}
