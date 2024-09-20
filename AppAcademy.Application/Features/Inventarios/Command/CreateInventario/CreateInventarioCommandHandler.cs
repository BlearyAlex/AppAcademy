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

namespace AppAcademy.Application.Features.Inventarios.Command.CreateInventario
{
    public class CreateInventarioCommandHandler : IRequestHandler<CreateInventarioCommand, string>
    {
        private readonly IInventarioRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateInventarioCommandHandler> _logger;

        public CreateInventarioCommandHandler(IInventarioRepository repository, IMapper mapper, ILogger<CreateInventarioCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> Handle(CreateInventarioCommand request, CancellationToken cancellationToken)
        {
            var inventario = _mapper.Map<Inventario>(request);
            var newInventario = await _repository.AddAsync(inventario);

            return newInventario.InventarioId;
        }
    }
}
