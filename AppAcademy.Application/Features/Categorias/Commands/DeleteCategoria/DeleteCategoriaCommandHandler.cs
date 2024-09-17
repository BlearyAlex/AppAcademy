using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Categorias.Commands.DeleteCategoria
{
    public class DeleteCategoriaCommandHandler : IRequestHandler<DeleteCategoriaCommand>
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteCategoriaCommand> _logger;

        public DeleteCategoriaCommandHandler(ICategoriaRepository categoriaRepository, IMapper mapper, ILogger<DeleteCategoriaCommand> logger)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(DeleteCategoriaCommand request, CancellationToken cancellationToken)
        {
            var deleteCategoria = await _categoriaRepository.GetById(request.CategoriaId);
            if (deleteCategoria == null)
            {
                _logger.LogError($"{request.CategoriaId} categoria no existe en el sistema");
                throw new NotFoundException(nameof(deleteCategoria), request.CategoriaId);
            }

            await _categoriaRepository.DeleteAsync(deleteCategoria);
            _logger.LogInformation($" {request.CategoriaId} fue eliminado con exito");

            return;
        }
    }
}
