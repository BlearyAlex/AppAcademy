using AppAcademy.Application.Contracts.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;

namespace AppAcademy.Controllers.ExcelController
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private readonly ICategoriaRepository _categoryRepository;
        private readonly ILogger<ChartsController> _logger;

        public ChartsController(ICategoriaRepository categoryRepository, ILogger<ChartsController> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        [HttpGet("export-excel")]
        public async Task<IActionResult> ExportExcel(DateTime startDate, DateTime endDate)
        {
            var salesData = await _categoryRepository.GetSalesEvolutionByCategory(startDate, endDate);

            if (!salesData.Any())
                return NoContent();

            // 1. Agrupar datos
            var fechas = salesData
                .Select(d => d.Fecha.Date)
                .Distinct()
                .OrderBy(d => d)
                .ToList();

            var categorias = salesData
                .Select(d => d.Categoria)
                .Distinct()
                .ToList();

            var lookup = salesData
                .GroupBy(d => new { d.Fecha.Date, d.Categoria })
                .ToDictionary(
                    g => (fecha: g.Key.Date, categoria: g.Key.Categoria),
                    g => g.Sum(x => x.Total)
                );

            using var package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("Ventas por Categoría");

            // 2. Encabezados
            sheet.Cells[1, 1].Value = "Fecha";
            for (int c = 0; c < categorias.Count; c++)
            {
                sheet.Cells[1, c + 2].Value = categorias[c];
            }

            // 3. Llenar filas
            for (int f = 0; f < fechas.Count; f++)
            {
                sheet.Cells[f + 2, 1].Value = fechas[f].ToString("dd/MM/yyyy"); // Formato de fecha
                for (int c = 0; c < categorias.Count; c++)
                {
                    var key = (fecha: fechas[f], categoria: categorias[c]);
                    sheet.Cells[f + 2, c + 2].Value = lookup.ContainsKey(key) ? lookup[key] : 0;
                }
            }

            // 4. Crear gráfico
            var chart = sheet.Drawings.AddChart("VentasPorCategoriaChart", eChartType.BarStacked);
            chart.Title.Text = "Evolución por Categoría y Fecha";
            chart.SetPosition(1, 0, categorias.Count + 2, 0);
            chart.SetSize(800, 400);

            // Agregar series (una por categoría)
            for (int c = 0; c < categorias.Count; c++)
            {
                // Rango de valores: desde fila 2 hasta (fechas.Count + 1), columna (c + 2)
                string valores = sheet.Cells[2, (c + 2), (fechas.Count + 1), (c + 2)].Address;
                // Rango de fechas: desde fila 2 hasta (fechas.Count + 1), columna 1
                string fechasRange = sheet.Cells[2, 1, (fechas.Count + 1), 1].Address;

                var serie = chart.Series.Add(valores, fechasRange);
                serie.Header = categorias[c];
            }

            var fileBytes = package.GetAsByteArray();
            return File(fileBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "ReporteVentasPorCategoria.xlsx");
        }

    }
}
