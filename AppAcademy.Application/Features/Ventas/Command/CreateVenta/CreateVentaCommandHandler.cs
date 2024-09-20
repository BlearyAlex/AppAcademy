using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Ventas.Command.CreateVenta
{
    public class CreateVentaCommandHandler : IRequestHandler<CreateVentaCommand, string>
    {
        private readonly IVentaRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateVentaCommandHandler> _logger;

        public CreateVentaCommandHandler(IVentaRepository repository, IMapper mapper, ILogger<CreateVentaCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> Handle(CreateVentaCommand request, CancellationToken cancellationToken)
        {
            var ventaEntity = _mapper.Map<Venta>(request);
            var newVenta = await _repository.AddAsync(ventaEntity);

            _logger.LogInformation($"Venta {newVenta.ventaId} fue creado exitosamente");

            return newVenta.ventaId;
        }
    }
}
