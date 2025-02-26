using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.StudentPaymentStatuses.Queries.GetAllStudentPaymentStatuses
{
    public class GetAllStudentPaymentStatusesQuery : IRequest<List<GetAllStudentPaymentStatusesVm>>
    {
    }
}
