using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.ControlVentas;
using AppAcademy.Domain.Enum;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Abonos.Command.CreateAbono
{
    public class CreateAbonoCommandHandler : IRequestHandler<CreateAbonoCommand, bool>
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

        public async Task<bool> Handle(CreateAbonoCommand request, CancellationToken cancellationToken)
        {
            return await _abonoRepository.CreateAbono(request.VentaId, request.MontoAbonado, request.UserName);
        }
    }
}
