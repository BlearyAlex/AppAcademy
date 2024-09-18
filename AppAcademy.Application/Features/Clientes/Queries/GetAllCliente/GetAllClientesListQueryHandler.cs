using AppAcademy.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace AppAcademy.Application.Features.Clientes.Queries.GetAllCliente
{
    public class GetAllClientesListQueryHandler : IRequestHandler<GetAllClientesListQuery, List<GetAllClientesVm>>
    {
        private readonly IClienteRepository _repository;
        private readonly IMapper _mapper;

        public GetAllClientesListQueryHandler(IClienteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllClientesVm>> Handle(GetAllClientesListQuery request, CancellationToken cancellationToken)
        {
            var clienteList = await _repository.GetAllAsync();

            return _mapper.Map<List<GetAllClientesVm>>(clienteList);
        }
    }
}
