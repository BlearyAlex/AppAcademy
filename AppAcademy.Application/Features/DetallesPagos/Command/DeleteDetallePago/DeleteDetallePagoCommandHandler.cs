using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.DetallesPagos.Command.DeleteDetallePago
{
    public class DeleteDetallePagoCommandHandler : IRequestHandler<DeleteDetallePagoCommand>
    {
        private readonly IDetallePagoRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteDetallePagoCommandHandler> _logger;

        public DeleteDetallePagoCommandHandler(IDetallePagoRepository repository, IMapper mapper, ILogger<DeleteDetallePagoCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(DeleteDetallePagoCommand request, CancellationToken cancellationToken)
        {
            var findDetallePago = await _repository.GetById(request.DetallePagoId);
            if(findDetallePago == null)
            {
                _logger.LogError($"{request.DetallePagoId} Detalle Corte no existe en el sistma");
                throw new NotFoundException(nameof(findDetallePago), request.DetallePagoId);
            }

            await _repository.DeleteAsync(findDetallePago);
            _logger.LogInformation($"El {request.DetallePagoId} fue eliminado con exito");

            return;
        }
    }
}
