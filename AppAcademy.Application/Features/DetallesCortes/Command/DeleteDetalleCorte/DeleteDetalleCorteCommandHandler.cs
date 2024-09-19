using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;


namespace AppAcademy.Application.Features.DetallesCortes.Command.DeleteDetalleCorte
{
    public class DeleteDetalleCorteCommandHandler : IRequestHandler<DeleteDetalleCorteCommand>
    {
        private readonly IDetalleCorteRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteDetalleCorteCommandHandler> _logger;

        public DeleteDetalleCorteCommandHandler(IDetalleCorteRepository repository, IMapper mapper, ILogger<DeleteDetalleCorteCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(DeleteDetalleCorteCommand request, CancellationToken cancellationToken)
        {
            var findDetalleCorte = await _repository.GetById(request.DetalleCorteId);
            if(findDetalleCorte == null)
            {
                _logger.LogError($"{request.DetalleCorteId} Detalle Corte no existe en el sistma");
                throw new NotFoundException(nameof(findDetalleCorte), request.DetalleCorteId);
            }

            await _repository.DeleteAsync(findDetalleCorte);
            _logger.LogInformation($"El {request.DetalleCorteId} fue eliminado con exito");

            return;

        }
    }
}
