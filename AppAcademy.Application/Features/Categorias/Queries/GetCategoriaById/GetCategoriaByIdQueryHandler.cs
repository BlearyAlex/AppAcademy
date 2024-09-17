using AppAcademy.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace AppAcademy.Application.Features.Categorias.Queries.GetCategoriaById
{
    public class GetCategoriaByIdQueryHandler : IRequestHandler<GetCategoriaByIdQuery, GetCategoriaByIdVm>
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public GetCategoriaByIdQueryHandler(ICategoriaRepository categoriaRepository, IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        public async Task<GetCategoriaByIdVm> Handle(GetCategoriaByIdQuery request, CancellationToken cancellationToken)
        {
            var categoria = await _categoriaRepository.GetById(request._CategoriaId);

            return _mapper.Map<GetCategoriaByIdVm>(categoria);
        }
    }
}
