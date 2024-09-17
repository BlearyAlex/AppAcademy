using AppAcademy.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace AppAcademy.Application.Features.Categorias.Queries.GetAllCategoria
{
    public class GetAllCategoriasListQueryHandler : IRequestHandler<GetAllCategoriasListQuery, List<GetAllCategoriasVm>>
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public GetAllCategoriasListQueryHandler(ICategoriaRepository categoriaRepository, IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllCategoriasVm>> Handle(GetAllCategoriasListQuery request, CancellationToken cancellationToken)
        {
            var categoriaList = await _categoriaRepository.GetAllAsync();

            return _mapper.Map<List<GetAllCategoriasVm>>(categoriaList);
        }
    }
}
