using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Ventas.Command.DeleteVenta
{
    public class DeleteVentaCommandHandler : IRequestHandler<DeleteVentaCommand>
    {
        private readonly IVentaRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteVentaCommandHandler> _logger;

        public DeleteVentaCommandHandler(IVentaRepository repository, IMapper mapper, ILogger<DeleteVentaCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(DeleteVentaCommand request, CancellationToken cancellationToken)
        {
            var deleteVenta = await _repository.GetById(request.VentaId);
            if (deleteVenta == null)
            {
                _logger.LogError($"{request.VentaId} venta no existe en el sistma");
                throw new NotFoundException(nameof(deleteVenta), request.VentaId);
            }

            await _repository.DeleteAsync(deleteVenta);
            _logger.LogInformation($"El {request.VentaId} fue eliminado con exito");

            return;
        }
    }
}
