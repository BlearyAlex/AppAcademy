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

namespace AppAcademy.Application.Features.DetallesCortes.Command.CreateDetalleCorte
{
    public class CreateDetalleCorteCommandHandler : IRequestHandler<CreateDetalleCorteCommand, string>
    {
        private readonly IDetalleCorteRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateDetalleCorteCommandHandler> _logger;

        public CreateDetalleCorteCommandHandler(IDetalleCorteRepository repository, IMapper mapper, ILogger<CreateDetalleCorteCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> Handle(CreateDetalleCorteCommand request, CancellationToken cancellationToken)
        {
            var detalleCorteEntity = _mapper.Map<DetalleCorte>(request);
            var newDetalleCorte = await _repository.AddAsync(detalleCorteEntity);

            return newDetalleCorte.DetalleCorteId;
        }
    }
}
