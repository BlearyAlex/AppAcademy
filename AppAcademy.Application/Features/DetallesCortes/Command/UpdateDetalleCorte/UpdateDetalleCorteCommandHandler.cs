using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Exceptions;
using AppAcademy.Domain.PuntoDeVenta;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.DetallesCortes.Command.UpdateDetalleCorte
{
    public class UpdateDetalleCorteCommandHandler : IRequestHandler<UpdateDetalleCorteCommand>
    {
        private readonly IDetalleCorteRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateDetalleCorteCommandHandler> _logger;

        public UpdateDetalleCorteCommandHandler(IDetalleCorteRepository repository, IMapper mapper, ILogger<UpdateDetalleCorteCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(UpdateDetalleCorteCommand request, CancellationToken cancellationToken)
        {
            var findDetalleCorte = await _repository.GetById(request.DetalleCorteId);

            if(findDetalleCorte == null)
            {
                _logger.LogError($"No se encontro el id del detalle corte {request.DetalleCorteId}");
                throw new NotFoundException(nameof(DetalleCorte), request.DetalleCorteId);
            }

            _mapper.Map(request, findDetalleCorte);

            await _repository.UpdateAsync(findDetalleCorte);

            _logger.LogInformation($"La operacion fue exitosa {request.DetalleCorteId}");
        }
    }
}
