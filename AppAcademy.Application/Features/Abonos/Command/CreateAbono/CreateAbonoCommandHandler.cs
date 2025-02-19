using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.ControlVentas;
using AppAcademy.Domain.Enum;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAcademy.Application.Features.Abonos.Command.CreateAbono
{
    public class CreateAbonoCommandHandler : IRequestHandler<CreateAbonoCommand, string>
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IAbonoRepository _abonoRepository;
        private readonly ILogger<CreateAbonoCommandHandler> _logger;

        public CreateAbonoCommandHandler(IVentaRepository ventaRepository, IAbonoRepository abonoRepository, ILogger<CreateAbonoCommandHandler> logger)
        {
            _ventaRepository = ventaRepository;
            _abonoRepository = abonoRepository;
            _logger = logger;
        }

        public async Task<string> Handle(CreateAbonoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var venta = await _ventaRepository.GetById(request.VentaId);

                if (venta == null)
                {
                    return "Venta no encontrada";
                }

                if (request.MontoAbonado > venta.SaldoPendiente)
                {
                    return "El monto abonado no puede ser mayor al saldo pendiente";
                }

                var abono = new Abono
                {
                    VentaId = request.VentaId,
                    Monto = request.MontoAbonado,
                    Fecha = DateTime.Now
                };

                await _abonoRepository.CreateAbono(abono);

                venta.SaldoPendiente -= request.MontoAbonado;

                if (venta.SaldoPendiente == 0)
                {
                    venta.EstadoVenta = VentaEstado.Pagado;
                }

                // Actualizar la venta después de procesar el abono
                bool actualizado = await _ventaRepository.UpdateVentaSaldo(venta);

                if (!actualizado)
                {
                    await _abonoRepository.DeleteAbono(abono.AbonoId);
                    return "No se pudo actualizar la venta.";
                }

                return $"Abono registrado con éxito. Venta ID: {request.VentaId}";
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
