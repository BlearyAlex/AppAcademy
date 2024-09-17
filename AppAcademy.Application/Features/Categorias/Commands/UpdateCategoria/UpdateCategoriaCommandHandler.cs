using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Exceptions;
using AppAcademy.Domain.PuntoDeVenta;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Categorias.Commands.UpdateCategoria
{
    public class UpdateCategoriaCommandHandler : IRequestHandler<UpdateCategoriaCommand>
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCategoriaCommandHandler> _logger;

        public UpdateCategoriaCommandHandler(ICategoriaRepository categoriaRepository, IMapper mapper, ILogger<UpdateCategoriaCommandHandler> logger)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(UpdateCategoriaCommand request, CancellationToken cancellationToken)
        {
            var findCategoria = await _categoriaRepository.GetById(request.CategoriaId);

            if(findCategoria == null)
            {
                _logger.LogError($"No se encontro el id de la categoria {request.CategoriaId}");
                throw new NotFoundException(nameof(Categoria), request.CategoriaId);
            }

            _mapper.Map(request, findCategoria);

            await _categoriaRepository.UpdateAsync(findCategoria);

            _logger.LogInformation($"La operacion fue exitosa {request.CategoriaId}");
        }
    }
}
