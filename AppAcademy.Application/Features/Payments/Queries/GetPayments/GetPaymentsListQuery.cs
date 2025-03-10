using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Payments.Queries.GetPayments
{
    public class GetPaymentsListQuery : IRequest<List<GetPaymentsVm>>
    {
    }
}
