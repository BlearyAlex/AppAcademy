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

namespace AppAcademy.Application.Features.Devoluciones.Command.CreateDevolucion
{
    public class CreateDevolucionCommandHandler : IRequestHandler<CreateDevolucionCommand, string>
    {
        private readonly IDevolucionRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateDevolucionCommandHandler> _logger;

        public CreateDevolucionCommandHandler(IDevolucionRepository repository, IMapper mapper, ILogger<CreateDevolucionCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> Handle(CreateDevolucionCommand request, CancellationToken cancellationToken)
        {
            var newDevolucionEntity = _mapper.Map<Devolucion>(request);
            var newDevolucion = await _repository.AddAsync(newDevolucionEntity);

            return newDevolucion.DevolucionId;
        }
    }
}
