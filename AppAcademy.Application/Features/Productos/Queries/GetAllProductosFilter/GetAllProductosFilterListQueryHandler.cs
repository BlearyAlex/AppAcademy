using AppAcademy.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Productos.Queries.GetAllProductosFilter
{
    public class GetAllProductosFilterListQueryHandler : IRequestHandler<GetAllProductosFilterListQuery, List<GetAllProductosFilterVm>>
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;

        public GetAllProductosFilterListQueryHandler(IProductoRepository productoRepository, IMapper mapper)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllProductosFilterVm>> Handle(GetAllProductosFilterListQuery request, CancellationToken cancellationToken)
        {
            var productsList = await _productoRepository.GetAllProductosWithFilter();
            
            return productsList;
        }
    }
}
