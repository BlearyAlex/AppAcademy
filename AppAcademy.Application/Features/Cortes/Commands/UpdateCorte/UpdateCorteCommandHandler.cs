using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Exceptions;
using AppAcademy.Application.Features.Productos.Commands.UpdateProducto;
using AppAcademy.Domain.PuntoDeVenta;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Cortes.Commands.UpdateCorte
{
    public class UpdateCorteCommandHandler : IRequestHandler<UpdateCorteCommand>
    {
        private readonly ICorteRepository _corteRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCorteCommandHandler> _logger;

        public UpdateCorteCommandHandler(ICorteRepository corteRepository, IMapper mapper, ILogger<UpdateCorteCommandHandler> logger)
        {
            _corteRepository = corteRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(UpdateCorteCommand request, CancellationToken cancellationToken)
        {
            var findCorte = await _corteRepository.GetById(request.CorteId);

            if (findCorte == null)
            {
                _logger.LogError($"No se encontro el id del corte {request.CorteId}");
                throw new NotFoundException(nameof(Corte), request.CorteId);
            }

            _mapper.Map(request, findCorte);

            await _corteRepository.UpdateAsync(findCorte);

            _logger.LogInformation($"La operacion fue exitosa {request.CorteId}");
        }
    }
}
