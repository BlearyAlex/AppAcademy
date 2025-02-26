using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.StudentPaymentStatuses.Commands.DeleteStudentPaymentStatus
{
    public class DeleteStudentPaymentStatusCommand : IRequest
    {
        public int StudentPaymentStatusId { get; set; }
    }
}
