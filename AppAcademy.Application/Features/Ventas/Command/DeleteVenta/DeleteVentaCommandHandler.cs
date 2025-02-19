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
            try
            {
                // Llamamos al repositorio para eliminar la venta
                var eliminado = await _repository.DeleteVenta(request.VentaId);

                if (!eliminado)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
