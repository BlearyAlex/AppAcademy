using AppAcademy.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Productos.Queries.GetProductById
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, GetProductByIdVm>
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IProductoRepository productoRepository, IMapper mapper)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
        }

        public async Task<GetProductByIdVm> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _productoRepository.GetById(request._ProductoId);

            return _mapper.Map<GetProductByIdVm>(product);
        }
    }
}
