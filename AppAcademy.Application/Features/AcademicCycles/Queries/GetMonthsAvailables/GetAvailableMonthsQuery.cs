using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.AcademicCycles.Queries.GetMonthsAvailables
{
    public class GetAvailableMonthsQuery : IRequest<List<AvailableMonthDto>>
    {
        public int StudentId { get; set; }
        public int AcademicCycleId { get; set; }

        public GetAvailableMonthsQuery(int studentId, int academicCycleId)
        {
            StudentId = studentId;
            AcademicCycleId = academicCycleId;
        }
    }
}
