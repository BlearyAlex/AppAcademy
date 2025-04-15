using AppAcademy.Application.Contracts.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Abonos.Command.CreateAbono
{
    public class CreateAbonoCommandHandler : IRequestHandler<CreateAbonoCommand, CreateAbonoResult>
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IAbonoRepository _abonoRepository;
        private readonly ILogger<CreateAbonoCommandHandler> _logger;

        public CreateAbonoCommandHandler(IVentaRepository ventaRepository, IAbonoRepository abonoRepository, ILogger<CreateAbonoCommandHandler> logger)
        {
            _ventaRepository = ventaRepository;
            _abonoRepository = abonoRepository;
            _logger = logger;
        }

        public async Task<CreateAbonoResult> Handle(CreateAbonoCommand request, CancellationToken cancellationToken)
        {
            return await _abonoRepository.CreateAbono(request.VentaId, request.MontoAbonado, request.UserName);
        }
    }
}
