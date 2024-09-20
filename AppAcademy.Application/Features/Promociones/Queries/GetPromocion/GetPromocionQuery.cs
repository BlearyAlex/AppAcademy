using MediatR;

namespace AppAcademy.Application.Features.Promociones.Queries.GetPromocion
{
    public class GetPromocionQuery : IRequest<GetPromocionVm>
    {
        public string _PromocionId { get; set; }

        public GetPromocionQuery(string promocionId)
        {
            _PromocionId = promocionId;
        }
    }
}
