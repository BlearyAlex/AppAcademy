using AppAcademy.Application.Contracts.Persistence;
using MediatR;

namespace AppAcademy.Application.Features.Entradas.Queries.GetEntradasForMonth
{
    public class GetEntradasForMonthQueryHandler : IRequestHandler<GetEntradasForMonthQuery, GetEntradasForMonthVm>
    {
        private readonly IEntradaRepository _entradaRepository;

        public GetEntradasForMonthQueryHandler(IEntradaRepository entradaRepository)
        {
            _entradaRepository = entradaRepository;
        }

        public async Task<GetEntradasForMonthVm> Handle(GetEntradasForMonthQuery request, CancellationToken cancellationToken)
        {
            return await _entradaRepository.GetEntradasForMonth();
        }
    }
}
