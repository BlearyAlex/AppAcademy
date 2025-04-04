using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.Enum;
using AppAcademy.Domain.PuntoDeVenta;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppAcademy.Application.Features.Ventas.Command.CreateVenta
{
    public class CreateVentaCommandHandler : IRequestHandler<CreateVentaCommand, string>
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateVentaCommandHandler> _logger;

        public CreateVentaCommandHandler(IVentaRepository ventaRepository, IMapper mapper, ILogger<CreateVentaCommandHandler> logger)
        {
            _ventaRepository = ventaRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> Handle(CreateVentaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var venta = new Venta
                {
                    ClienteId = request.ClienteId,
                    Descuento = request.Descuento,
                    Impuesto = request.Impuesto,
                    DetalleVentas = request.Detalles.Select(d => new VentaDetalle
                    {
                        ProductoId = d.ProductoId,
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario,
                        Total = d.Cantidad * d.PrecioUnitario,
                    }).ToList()
                };

                var nuevaVenta = await _ventaRepository.CreateVenta(venta, request.UserName);

                return $"Venta creada exitosamente. ID: {nuevaVenta.VentaId}";
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
