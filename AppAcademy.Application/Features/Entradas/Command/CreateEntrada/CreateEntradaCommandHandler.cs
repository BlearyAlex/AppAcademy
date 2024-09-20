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

namespace AppAcademy.Application.Features.Entradas.Command.CreateEntrada
{
    public class CreateEntradaCommandHandler : IRequestHandler<CreateEntradaCommand, string>
    {
        private readonly IEntradaRepository _productoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateEntradaCommandHandler> _logger;

        public CreateEntradaCommandHandler(IEntradaRepository productoRepository, IMapper mapper, ILogger<CreateEntradaCommandHandler> logger)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> Handle(CreateEntradaCommand request, CancellationToken cancellationToken)
        {
            var entradaEntity = _mapper.Map<Entrada>(request);
            var newEntrada = await _productoRepository.AddAsync(entradaEntity);

            return newEntrada.EntradaId;
        }
    }
}
