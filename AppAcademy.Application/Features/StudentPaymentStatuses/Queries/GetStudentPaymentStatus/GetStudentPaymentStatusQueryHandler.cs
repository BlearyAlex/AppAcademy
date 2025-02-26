using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Application.Features.Careers.Queries.GetCareer;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.StudentPaymentStatuses.Queries.GetStudentPaymentStatus
{
    public class GetStudentPaymentStatusQueryHandler : IRequestHandler<GetStudentPaymentStatusQuery, GetStudentPaymentStatusVm>
    {
        private readonly IStudentPaymentStatusRepository _studentPaymentStatusRepository;
        private readonly IMapper _mapper;

        public GetStudentPaymentStatusQueryHandler(IStudentPaymentStatusRepository studentPaymentStatusRepository, IMapper mapper)
        {
            _studentPaymentStatusRepository = studentPaymentStatusRepository;
            _mapper = mapper;
        }

        public async Task<GetStudentPaymentStatusVm> Handle(GetStudentPaymentStatusQuery request, CancellationToken cancellationToken)
        {
            var studentPayment = await _studentPaymentStatusRepository.GetByIdInt(request._StudentPaymentStatusId);

            return _mapper.Map<GetStudentPaymentStatusVm>(studentPayment);
        }
    }
}
