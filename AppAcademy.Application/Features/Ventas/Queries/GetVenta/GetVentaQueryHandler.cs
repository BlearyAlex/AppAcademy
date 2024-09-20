using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Application.Features.Productos.Queries.GetProductById;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Ventas.Queries.GetVenta
{
    public class GetVentaQueryHandler : IRequestHandler<GetVentaQuery, GetVentaVm>
    {
        private readonly IVentaRepository _repository;
        private readonly IMapper _mapper;

        public GetVentaQueryHandler(IVentaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetVentaVm> Handle(GetVentaQuery request, CancellationToken cancellationToken)
        {
            var venta = await _repository.GetById(request._VentaId);

            return _mapper.Map<GetVentaVm>(venta);
        }
    }
}
