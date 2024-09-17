using AppAcademy.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace AppAcademy.Application.Features.Proveedores.Queries.GetAllProveedor
{
    public class GetAllProveedoresListQueryHandler : IRequestHandler<GetAllProveedoresListQuery, List<GetAllProveedoresVm>>
    {
        private readonly IProveedorRepository _proveedorRepository;
        private readonly IMapper _mapper;

        public GetAllProveedoresListQueryHandler(IProveedorRepository proveedorRepository, IMapper mapper)
        {
            _proveedorRepository = proveedorRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllProveedoresVm>> Handle(GetAllProveedoresListQuery request, CancellationToken cancellationToken)
        {
            var proveedorList = await _proveedorRepository.GetAllAsync();

            return _mapper.Map<List<GetAllProveedoresVm>>(proveedorList);
        }
    }
}
