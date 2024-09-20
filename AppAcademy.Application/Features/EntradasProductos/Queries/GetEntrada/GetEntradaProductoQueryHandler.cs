using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Features.Entradas.Queries.GetEntrada;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.EntradasProductos.Queries.GetEntrada
{
    public class GetEntradaProductoQueryHandler : IRequestHandler<GetEntradaProductoQuery, GetEntradaProductoVm>
    {
        private readonly IEntradaProductoRepository _repository;
        private readonly IMapper _mapper;

        public GetEntradaProductoQueryHandler(IEntradaProductoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetEntradaProductoVm> Handle(GetEntradaProductoQuery request, CancellationToken cancellationToken)
        {
            var entrada = await _repository.GetById(request._EntradaProductoId);

            return _mapper.Map<GetEntradaProductoVm>(entrada);
        }
    }
}
