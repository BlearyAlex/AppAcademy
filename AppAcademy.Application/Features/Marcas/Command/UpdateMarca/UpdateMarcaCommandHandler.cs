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

namespace AppAcademy.Application.Features.Marcas.Command.UpdateMarca
{
    public class UpdateMarcaCommandHandler : IRequestHandler<UpdateMarcaCommand>
    {
        private readonly IMarcaRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateMarcaCommandHandler> _logger;

        public UpdateMarcaCommandHandler(IMarcaRepository repository, IMapper mapper, ILogger<UpdateMarcaCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(UpdateMarcaCommand request, CancellationToken cancellationToken)
        {
            var updateMarca = await _repository.GetById(request.MarcaId);
            if (updateMarca == null)
            {
                _logger.LogError($"No se encontro el id de la marca {request.MarcaId}");
                throw new NotFoundException(nameof(Inventario), request.MarcaId);
            }

            _mapper.Map(request, updateMarca);

            await _repository.UpdateAsync(updateMarca);

            _logger.LogInformation($"La operacion fue exitosa {request.MarcaId}");
        }
    }
}
