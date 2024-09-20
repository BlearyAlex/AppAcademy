using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Exceptions;
using AppAcademy.Application.Features.DetallesCortes.Command.UpdateDetalleCorte;
using AppAcademy.Domain.PuntoDeVenta;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.DetallesPagos.Command.UpdateDetallePago
{
    public class UpdateDetallePagoCommandHandler : IRequestHandler<UpdateDetallePagoCommand>
    {
        private readonly IDetallePagoRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateDetallePagoCommandHandler> _logger;

        public UpdateDetallePagoCommandHandler(IDetallePagoRepository repository, IMapper mapper, ILogger<UpdateDetallePagoCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(UpdateDetallePagoCommand request, CancellationToken cancellationToken)
        {
            var findDetallePago = await _repository.GetById(request.DetallePagoId);

            if (findDetallePago == null)
            {
                _logger.LogError($"No se encontro el id del detalle pago {request.DetallePagoId}");
                throw new NotFoundException(nameof(DetallePago), request.DetallePagoId);
            }

            _mapper.Map(request, findDetallePago);

            await _repository.UpdateAsync(findDetallePago);

            _logger.LogInformation($"La operacion fue exitosa {request.DetallePagoId}");
        }
    }
}
