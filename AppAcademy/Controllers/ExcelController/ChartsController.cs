using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.PuntoDeVenta;
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
            using var package = new ExcelPackage();

            var sheet1 = package.Workbook.Worksheets.Add("Evolucion por Categoria");
            await EvolutionPerCategoryAndDate(sheet1, startDate, endDate);

            var sheet2 = package.Workbook.Worksheets.Add("Evolucion por Categoria y Fechas");
            await EvolutionPerCategoryAndPerDate(sheet2, startDate, endDate);
       
            var sheet3 = package.Workbook.Worksheets.Add("Ventas por Categoria");
            await SalesByCategory(sheet3, startDate, endDate);

            var sheet4 = package.Workbook.Worksheets.Add("Categoria mas Destacadas");
            await HighlightedCategories(sheet4, startDate, endDate);

            var fileBytes = package.GetAsByteArray();
            return File(fileBytes, 
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "ReporteVentas.xlsx");
        }

        private async Task EvolutionPerCategoryAndDate(ExcelWorksheet sheet, DateTime startDate, DateTime endDate)
        {
            var salesData = await _categoryRepository.GetSalesEvolutionByCategory(startDate, endDate);

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
        }

        private async Task EvolutionPerCategoryAndPerDate(ExcelWorksheet sheet, DateTime startDate, DateTime endDate)
        {
            var salesData = await _categoryRepository.GetSalesEvolutionByCategory(startDate, endDate);
            var fechas = salesData.Select(d => d.Fecha.Date).Distinct().OrderBy(d => d).ToList();
            var totalVentas = salesData
                .GroupBy(d => d.Fecha.Date)
                .Select(g => new { Fecha = g.Key, TotalVentas = g.Sum(x => x.Total) })
                .OrderBy(d => d.Fecha)
                .ToList();

            sheet.Cells[1, 1].Value = "Fecha";
            sheet.Cells[1, 2].Value = "Total Ventas";

            for (int i = 0; i < totalVentas.Count; i++)
            {
                sheet.Cells[i + 2, 1].Value = totalVentas[i].Fecha.ToString("dd/MM/yyyy");
                sheet.Cells[i + 2, 2].Value = totalVentas[i].TotalVentas;
            }

            var chart = sheet.Drawings.AddChart("EvolucionTotalChart", eChartType.Line);
            chart.Title.Text = "Evolución Total de Ventas";
            chart.SetPosition(1, 0, 2, 0);
            chart.SetSize(800, 400);

            string valores = sheet.Cells[2, 2, totalVentas.Count + 1, 2].Address;
            string fechasRange = sheet.Cells[2, 1, totalVentas.Count + 1, 1].Address;
            var serie = chart.Series.Add(valores, fechasRange);
            serie.Header = "Total Ventas";
        }

        private async Task SalesByCategory(ExcelWorksheet sheet, DateTime startDate, DateTime endDate)
        {
            var salesData = await _categoryRepository.GetSalesByCategory(startDate, endDate);
            var categorias = salesData
                .GroupBy(d => d.Categoria)
                .Select(g => new { Categoria = g.Key, TotalVentas = g.Sum(x => x.Total) })
                .ToList();

            sheet.Cells[1, 1].Value = "Categoría";
            sheet.Cells[1, 2].Value = "Total Ventas";

            for (int i = 0; i < categorias.Count; i++)
            {
                sheet.Cells[i + 2, 1].Value = categorias[i].Categoria;
                sheet.Cells[i + 2, 2].Value = categorias[i].TotalVentas;
            }

            var chart = sheet.Drawings.AddChart("DistribucionPorCategoriaChart", eChartType.Doughnut);
            chart.Title.Text = "Distribución de Ventas por Categoría";
            chart.SetPosition(1, 0, 2, 0);
            chart.SetSize(600, 400);

            string valores = sheet.Cells[2, 2, categorias.Count + 1, 2].Address;
            var serie = chart.Series.Add(valores, sheet.Cells[2, 1, categorias.Count + 1, 1].Address);
            serie.Header = "Categorías";
        }

        private async Task HighlightedCategories(ExcelWorksheet sheet, DateTime startDate, DateTime endDate)
        {
            var salesData = await _categoryRepository.GetHighlightedCategories(startDate, endDate);

            sheet.Cells[1, 1].Value = "Categoría";
            sheet.Cells[1, 2].Value = "Total Ventas";

            for (int i = 0; i < salesData.Count; i++)
            {
                sheet.Cells[i + 2, 1].Value = salesData[i].Categoria;
                sheet.Cells[i + 2, 2].Value = salesData[i].Total;
            }

            var chart = sheet.Drawings.AddChart("CategoriasDestacadasChart", eChartType.BarClustered);
            chart.Title.Text = "Categorías Más Destacadas";
            chart.SetPosition(1, 0, 3, 0);  // Posicionar el gráfico en la hoja
            chart.SetSize(800, 400);

            string valores = sheet.Cells[2, 2, salesData.Count + 1, 2].Address;

            string categoriasRange = sheet.Cells[2, 1, salesData.Count + 1, 1].Address;

            var serie = chart.Series.Add(valores, categoriasRange);
            serie.Header = "Total Ventas";  // Título de la serie (Total Ventas)
        }
    }
}
