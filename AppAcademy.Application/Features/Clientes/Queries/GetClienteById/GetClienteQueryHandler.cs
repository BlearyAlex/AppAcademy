using AppAcademy.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace AppAcademy.Application.Features.Clientes.Queries.GetClienteById
{
    public class GetClienteQueryHandler : IRequestHandler<GetClienteQuery, GetClienteVm>
    {
        private readonly IClienteRepository _repository;
        private readonly IMapper _mapper;

        public GetClienteQueryHandler(IClienteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetClienteVm> Handle(GetClienteQuery request, CancellationToken cancellationToken)
        {
            var cliente = await _repository.GetById(request._ClienteId);

            return _mapper.Map<GetClienteVm>(cliente);
        }
    }
}
