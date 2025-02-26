using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AutoMapper;
using MediatR;

namespace AppAcademy.Application.Features.Payments.Queries.GetAllPayments
{
    public class GetAllPaymentListQueryHandler : IRequestHandler<GetAllPaymentListQuery, List<GetAllPaymentsVm>>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public GetAllPaymentListQueryHandler(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllPaymentsVm>> Handle(GetAllPaymentListQuery request, CancellationToken cancellationToken)
        {
            var paymentList = await _paymentRepository.GetAllAsync();

            return _mapper.Map<List<GetAllPaymentsVm>>(paymentList);
        }
    }
}
