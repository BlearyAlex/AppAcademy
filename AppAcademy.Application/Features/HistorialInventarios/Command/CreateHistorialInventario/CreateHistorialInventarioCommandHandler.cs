using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.HistorialInventarios.Command.CreateHistorialInventario
{
    public class CreateHistorialInventarioCommandHandler : IRequestHandler<CreateHistorialInventarioCommand, string>
    {
        private readonly IHistorialInventarioRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateHistorialInventarioCommandHandler> _logger;

        public CreateHistorialInventarioCommandHandler(IHistorialInventarioRepository repository, IMapper mapper, ILogger<CreateHistorialInventarioCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> Handle(CreateHistorialInventarioCommand request, CancellationToken cancellationToken)
        {
            var historialInventario = _mapper.Map<HistorialInventario>(request);
            var newHistorialInventario = await _repository.AddAsync(historialInventario);

            return newHistorialInventario.HistorialInventarioId;
        }
    }
}
