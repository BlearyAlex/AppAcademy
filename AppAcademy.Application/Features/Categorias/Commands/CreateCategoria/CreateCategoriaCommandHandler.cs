using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Categorias.Commands.CreateCategoria
{
    public class CreateCategoriaCommandHandler : IRequestHandler<CreateCategoriaCommand, string>
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCategoriaCommandHandler> _logger;

        public CreateCategoriaCommandHandler(ICategoriaRepository categoriaRepository, IMapper mapper, ILogger<CreateCategoriaCommandHandler> logger)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> Handle(CreateCategoriaCommand request, CancellationToken cancellationToken)
        {
            var categoriaEnity = _mapper.Map<Categoria>(request);
            var newCategoria = await _categoriaRepository.AddAsync(categoriaEnity);

            _logger.LogInformation($"Categoria {newCategoria.CategoriaId} fue creado exitosamente");

            return newCategoria.CategoriaId;
        }
    }
}
