using AppAcademy.Application.Contracts.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace AppAcademy.Controllers.ReportsController
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IAbonoRepository _abonoRepository;
        private readonly ILogger<ReportsController> _logger;
        private readonly IReciboAbonoPdfService _reciboPdfService;

        public ReportsController(IAbonoRepository abonoRepository, ILogger<ReportsController> logger, IReciboAbonoPdfService reciboPdfService)
        {
            _abonoRepository = abonoRepository;
            _logger = logger;
            _reciboPdfService = reciboPdfService;
        }

        //[HttpGet("recibo-abono/{abonoId}")]
        //public async Task<IActionResult> DescargarReciboAbono(int abonoId)
        //{
        //    try
        //    {
        //        var abono = await _abonoRepository.GeneratePdf(abonoId);

        //        var pdf = _reciboPdfService.GenerarReciboAbonoPDF(abono.Venta, abono);

        //        return File(pdf, "application/pdf", $"Recibo-Abono-{abonoId}.pdf");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Error generando recibo de abono {abonoId}");
        //        return BadRequest("No se pudo generar el recibo.");
        //    }
        //}
    }
}
