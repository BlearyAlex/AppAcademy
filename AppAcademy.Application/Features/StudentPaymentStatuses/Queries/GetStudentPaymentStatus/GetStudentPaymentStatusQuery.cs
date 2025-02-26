using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.StudentPaymentStatuses.Queries.GetStudentPaymentStatus
{
    public class GetStudentPaymentStatusQuery : IRequest<GetStudentPaymentStatusVm>
    {
        public int _StudentPaymentStatusId { get; set; }

        public GetStudentPaymentStatusQuery(int studentPaymentStatusId)
        {
            _StudentPaymentStatusId = studentPaymentStatusId;
        }
    }
}
