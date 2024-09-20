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

namespace AppAcademy.Application.Features.Inventarios.Command.UpdateInventario
{
    public class UpdateInventarioCommandHandler : IRequestHandler<UpdateInventarioCommand>
    {
        private readonly IInventarioRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateInventarioCommandHandler> _logger;

        public UpdateInventarioCommandHandler(IInventarioRepository repository, IMapper mapper, ILogger<UpdateInventarioCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(UpdateInventarioCommand request, CancellationToken cancellationToken)
        {
            var updateInventario = await _repository.GetById(request.InventarioId);
            if (updateInventario == null)
            {
                _logger.LogError($"No se encontro el id de inventario {request.InventarioId}");
                throw new NotFoundException(nameof(Inventario), request.InventarioId);
            }

            _mapper.Map(request, updateInventario);

            await _repository.UpdateAsync(updateInventario);

            _logger.LogInformation($"La operacion fue exitosa {request.InventarioId}");
        }
    }
}
