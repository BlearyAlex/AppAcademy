using MediatR;

namespace AppAcademy.Application.Features.Salidas.Queries.GetSalida
{
    public class GetSalidaQuery : IRequest<GetSalidaVm>
    {
        public string _SalidaId { get; set; }
        public GetSalidaQuery(string salidaId)
        {
            _SalidaId = salidaId;
        }
    }
}
