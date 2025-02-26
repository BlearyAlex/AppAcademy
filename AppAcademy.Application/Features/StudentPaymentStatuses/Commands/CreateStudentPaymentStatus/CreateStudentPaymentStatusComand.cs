using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.StudentPaymentStatuses.Commands.CreateStudentPaymentStatus
{
    public class CreateStudentPaymentStatusComand : IRequest<int>
    {
        public int StudentPaymentStatusId { get; set; }
        public int Mes { get; set; }  // 1 = Enero, 2 = Febrero...
        public int Año { get; set; }
        public bool Pagado { get; set; }  // TRUE = Pagado, FALSE = Pendiente

        public int? StudentId { get; set; }
    }
}
