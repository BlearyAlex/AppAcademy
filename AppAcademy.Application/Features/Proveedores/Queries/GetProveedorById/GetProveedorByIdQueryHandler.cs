using AppAcademy.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace AppAcademy.Application.Features.Proveedores.Queries.GetProveedorById
{
    public class GetProveedorByIdQueryHandler : IRequestHandler<GetProveedorByIdQuery, GetProveedorByIdVm>
    {
        private readonly IProveedorRepository _proveedorRepository;
        private readonly IMapper _mapper;

        public GetProveedorByIdQueryHandler(IProveedorRepository proveedorRepository, IMapper mapper)
        {
            _proveedorRepository = proveedorRepository;
            _mapper = mapper;
        }

        public async Task<GetProveedorByIdVm> Handle(GetProveedorByIdQuery request, CancellationToken cancellationToken)
        {
            var proveedor = await _proveedorRepository.GetById(request._ProveedorId);

            return _mapper.Map<GetProveedorByIdVm>(proveedor);
        }
    }
}
