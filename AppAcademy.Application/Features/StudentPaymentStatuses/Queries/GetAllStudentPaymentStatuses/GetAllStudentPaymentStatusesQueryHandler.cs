using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Application.Features.Careers.Queries.GetAllCareers;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.StudentPaymentStatuses.Queries.GetAllStudentPaymentStatuses
{
    public class GetAllStudentPaymentStatusesQueryHandler : IRequestHandler<GetAllStudentPaymentStatusesQuery, List<GetAllStudentPaymentStatusesVm>>
    {
        private readonly IStudentPaymentStatusRepository _studentPaymentStatusRepository;
        private readonly ILogger<GetAllStudentPaymentStatusesQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllStudentPaymentStatusesQueryHandler(IStudentPaymentStatusRepository studentPaymentStatusRepository, ILogger<GetAllStudentPaymentStatusesQueryHandler> logger, IMapper mapper)
        {
            _studentPaymentStatusRepository = studentPaymentStatusRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<GetAllStudentPaymentStatusesVm>> Handle(GetAllStudentPaymentStatusesQuery request, CancellationToken cancellationToken)
        {
            var studentPaymentList = await _studentPaymentStatusRepository.GetAllAsync();

            return _mapper.Map<List<GetAllStudentPaymentStatusesVm>>(studentPaymentList);
        }
    }
}
