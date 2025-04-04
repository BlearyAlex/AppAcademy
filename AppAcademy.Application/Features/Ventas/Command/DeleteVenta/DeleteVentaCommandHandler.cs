using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Ventas.Command.DeleteVenta
{
    public class DeleteVentaCommandHandler : IRequestHandler<DeleteVentaCommand, bool>
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

        public async Task<bool> Handle(DeleteVentaCommand request, CancellationToken cancellationToken)
        {
            return await _repository.DeleteVenta(request.VentaId, request.UserName);  
        }
    }
}
