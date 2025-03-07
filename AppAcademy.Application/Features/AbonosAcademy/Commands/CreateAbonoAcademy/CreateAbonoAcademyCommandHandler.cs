using AppAcademy.Application.Contracts.Persistence.IControlAcademia;
using AppAcademy.Domain.ControlAcademia;
using AppAcademy.Domain.Enum;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.AbonosAcademy.Commands.CreateAbonoAcademy
{
    public class CreateAbonoAcademyCommandHandler : IRequestHandler<CreateAbonoAcademyCommand, bool>
    {
        private readonly IAbonoAcademyRepository _abonoAcademyRepository;
        private readonly ILogger<CreateAbonoAcademyCommandHandler> _logger;
        private readonly IPaymentRepository _paymentRepository;

        public CreateAbonoAcademyCommandHandler(IAbonoAcademyRepository abonoAcademyRepository, ILogger<CreateAbonoAcademyCommandHandler> logger, IPaymentRepository paymentRepository)
        {
            _abonoAcademyRepository = abonoAcademyRepository;
            _logger = logger;
            _paymentRepository = paymentRepository;
        }

        public async Task<bool> Handle(CreateAbonoAcademyCommand request, CancellationToken cancellationToken)
        {
            return await _abonoAcademyRepository.CreateAbonoAcademy(request.PaymentId, request.MontoAbonado);
        }
    }
}
