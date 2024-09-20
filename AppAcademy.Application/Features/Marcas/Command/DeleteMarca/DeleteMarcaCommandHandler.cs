using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Marcas.Command.DeleteMarca
{
    public class DeleteMarcaCommandHandler : IRequestHandler<DeleteMarcaCommand>
    {
        private readonly IMarcaRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteMarcaCommandHandler> _logger;

        public DeleteMarcaCommandHandler(IMarcaRepository repository, IMapper mapper, ILogger<DeleteMarcaCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(DeleteMarcaCommand request, CancellationToken cancellationToken)
        {
            var deleteMarca = await _repository.GetById(request.MarcaId);
            if (deleteMarca == null)
            {
                throw new NotFoundException(nameof(deleteMarca), request.MarcaId);
            }

            await _repository.DeleteAsync(deleteMarca);
            _logger.LogInformation($"El {request.MarcaId} fue eliminado con exito");

            return;
        }
    }
}
