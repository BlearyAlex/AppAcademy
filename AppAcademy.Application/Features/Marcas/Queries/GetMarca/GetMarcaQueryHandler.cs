using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Features.Inventarios.Queries.GetInventario;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Marcas.Queries.GetMarca
{
    public class GetMarcaQueryHandler : IRequestHandler<GetMarcaQuery, GetMarcaVm>
    {
        private readonly IMarcaRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetMarcaQueryHandler> _logger;

        public GetMarcaQueryHandler(IMarcaRepository repository, IMapper mapper, ILogger<GetMarcaQueryHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetMarcaVm> Handle(GetMarcaQuery request, CancellationToken cancellationToken)
        {
            var marca = await _repository.GetById(request._MarcaId);

            return _mapper.Map<GetMarcaVm>(marca);
        }
    }
}
