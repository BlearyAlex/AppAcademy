using AppAcademy.Application.Contracts.Persistence;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using Microsoft.AspNetCore.Authorization;

namespace AppAcademy.Controllers.ExcelController
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, User")]
    public class ExcelProductoController : ControllerBase
    {
        private readonly IProductoRepository _productoRepository;
        private readonly ILogger<ExcelCategoriaController> _logger;

        public ExcelProductoController(IProductoRepository productoRepository, ILogger<ExcelCategoriaController> logger)
        {
            _productoRepository = productoRepository;
            _logger = logger;
        }

        [HttpGet("export-excel")]
        public async Task<IActionResult> ExportExcel(DateTime startDate, DateTime endDate)
        {
            using var package = new ExcelPackage();

            // 1. Primero creamos las hojas que serán referenciadas
            var sheet1 = package.Workbook.Worksheets.Add("Evolución por Producto y Fechas");
            await EvolutionPerProductAndPerDate(sheet1, startDate, endDate);

            var sheet2 = package.Workbook.Worksheets.Add("Ventas por Producto");
            await SalesByProduct(sheet2, startDate, endDate);

            var sheet3 = package.Workbook.Worksheets.Add("Productos más Destacados");
            await HighlightedProducts(sheet3, startDate, endDate);

            // 2. Luego generamos la portada y ya existen las hojas
            var portadaSheet = package.Workbook.Worksheets.Add("Portada");
            GenerateCoverSheet(portadaSheet, startDate, endDate, package.Workbook);

            string portadaName = portadaSheet.Name;

            // Mover la hoja de portada al inicio (índice 0)
            package.Workbook.Worksheets.MoveToStart(portadaName);

            var fileBytes = package.GetAsByteArray();
            return File(fileBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "ReporteVentas.xlsx");
        }

        private async Task EvolutionPerProductAndPerDate(ExcelWorksheet sheet, DateTime startDate, DateTime endDate)
        {
            // ENCABEZADO GENERAL (filas 1 a 3)
            sheet.Cells[1, 1].Value = "Reporte de Productos";
            sheet.Cells[2, 1].Value = $"Desde: {startDate:dd/MM/yyyy}";
            sheet.Cells[3, 1].Value = $"Hasta: {endDate:dd/MM/yyyy}";

            // Estilos para el título
            using (var titleRange = sheet.Cells[1, 1])
            {
                titleRange.Style.Font.Bold = true;
                titleRange.Style.Font.Size = 16;
                titleRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid; // Fondo sólido
                titleRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.MediumSlateBlue); // Color de fondo
                titleRange.Style.Font.Color.SetColor(System.Drawing.Color.White); // Color del texto
                titleRange.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; // Centrado
                titleRange.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; // Centrado vertical
            }

            sheet.Cells[1, 1, 1, 2].Merge = true; // Combinar celdas para centrar
            sheet.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

            sheet.Cells[2, 1, 2, 2].Merge = true;
            sheet.Cells[2, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[2, 1].Style.Font.Bold = true; // Negrita

            sheet.Cells[3, 1, 3, 2].Merge = true;
            sheet.Cells[3, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[3, 1].Style.Font.Bold = true; // Negrita

            // Ahora el encabezado de la tabla empieza en la fila 5
            sheet.Cells[5, 1].Value = "Fecha";
            sheet.Cells[5, 2].Value = "Total Ventas";

            using (var range = sheet.Cells[5, 1, 5, 2])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.MediumSlateBlue);
                range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                range.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            }

            var salesData = await _productoRepository.GetSalesEvolutionByProducto(startDate, endDate);
            var totalVentas = salesData
                .GroupBy(d => d.Fecha.Date)
                .Select(g => new { Fecha = g.Key, TotalVentas = g.Sum(x => x.Total) })
                .OrderBy(d => d.Fecha)
                .ToList();

            for (int i = 0; i < totalVentas.Count; i++)
            {
                sheet.Cells[i + 6, 1].Value = totalVentas[i].Fecha.ToString("dd/MM/yyyy");
                sheet.Cells[i + 6, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                sheet.Cells[i + 6, 2].Value = totalVentas[i].TotalVentas;
                sheet.Cells[i + 6, 2].Style.Numberformat.Format = "\"$\"#,##0.00";
                sheet.Cells[i + 6, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }

            sheet.Column(1).Width = 20;
            sheet.Column(2).Width = 20;

            var chart = sheet.Drawings.AddChart("EvolucionTotalChart", eChartType.Line);
            chart.Title.Text = "Evolución Total de Ventas";
            chart.SetPosition(1, 0, 3, 0);
            chart.SetSize(800, 400);

            string valores = sheet.Cells[6, 2, totalVentas.Count + 5, 2].Address;
            string fechasRange = sheet.Cells[6, 1, totalVentas.Count + 5, 1].Address;
            var serie = chart.Series.Add(valores, fechasRange);
            serie.Header = "Total Ventas";
        }


        private async Task SalesByProduct(ExcelWorksheet sheet, DateTime startDate, DateTime endDate)
        {
            // ENCABEZADO GENERAL
            sheet.Cells[1, 1].Value = "Reporte de Productos";
            sheet.Cells[2, 1].Value = $"Desde: {startDate:dd/MM/yyyy}";
            sheet.Cells[3, 1].Value = $"Hasta: {endDate:dd/MM/yyyy}";

            using (var titleRange = sheet.Cells[1, 1])
            {
                titleRange.Style.Font.Bold = true;
                titleRange.Style.Font.Size = 16;
                titleRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid; // Fondo sólido
                titleRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.MediumSlateBlue); // Color de fondo
                titleRange.Style.Font.Color.SetColor(System.Drawing.Color.White); // Color del texto
                titleRange.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; // Centrado
                titleRange.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; // Centrado vertical
            }

            sheet.Cells[1, 1, 1, 2].Merge = true; // Combinar celdas para centrar
            sheet.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

            sheet.Cells[2, 1, 2, 2].Merge = true;
            sheet.Cells[2, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[2, 1].Style.Font.Bold = true; // Negrita

            sheet.Cells[3, 1, 3, 2].Merge = true;
            sheet.Cells[3, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[3, 1].Style.Font.Bold = true; // Negrita


            var salesData = await _productoRepository.GetSalesByProducto(startDate, endDate);
            var categorias = salesData
                .GroupBy(d => d.Producto)
                .Select(g => new { Marca = g.Key, TotalVentas = g.Sum(x => x.Total) })
                .ToList();

            // ENCABEZADO DE TABLA
            sheet.Cells[5, 1].Value = "Producto";
            sheet.Cells[5, 2].Value = "Total Ventas";

            using (var range = sheet.Cells[5, 1, 5, 2])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.MediumSlateBlue);
                range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
            }

            for (int i = 0; i < categorias.Count; i++)
            {
                sheet.Cells[i + 6, 1].Value = categorias[i].Marca;
                sheet.Cells[i + 6, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                sheet.Cells[i + 6, 2].Value = categorias[i].TotalVentas;
                sheet.Cells[i + 6, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Cells[i + 6, 2].Style.Numberformat.Format = "\"$\"#,##0.00";
            }

            sheet.Column(1).Width = 30;
            sheet.Column(2).Width = 20;

            var chart = sheet.Drawings.AddChart("DistribucionPorCategoriaChart", eChartType.Doughnut);
            chart.Title.Text = "Distribución de Ventas por Producto";
            chart.SetPosition(1, 0, 3, 0);
            chart.SetSize(600, 400);

            string valores = sheet.Cells[6, 2, categorias.Count + 5, 2].Address;
            var serie = chart.Series.Add(valores, sheet.Cells[6, 1, categorias.Count + 5, 1].Address);
            serie.Header = "Productos";
        }

        private async Task HighlightedProducts(ExcelWorksheet sheet, DateTime startDate, DateTime endDate)
        {
            // ENCABEZADO GENERAL
            sheet.Cells[1, 1].Value = "Reporte de Productos";
            sheet.Cells[2, 1].Value = $"Desde: {startDate:dd/MM/yyyy}";
            sheet.Cells[3, 1].Value = $"Hasta: {endDate:dd/MM/yyyy}";

            using (var titleRange = sheet.Cells[1, 1])
            {
                titleRange.Style.Font.Bold = true;
                titleRange.Style.Font.Size = 16;
                titleRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid; // Fondo sólido
                titleRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.MediumSlateBlue); // Color de fondo
                titleRange.Style.Font.Color.SetColor(System.Drawing.Color.White); // Color del texto
                titleRange.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; // Centrado
                titleRange.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; // Centrado vertical
            }

            sheet.Cells[1, 1, 1, 2].Merge = true; // Combinar celdas para centrar
            sheet.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

            sheet.Cells[2, 1, 2, 2].Merge = true;
            sheet.Cells[2, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[2, 1].Style.Font.Bold = true; // Negrita

            sheet.Cells[3, 1, 3, 2].Merge = true;
            sheet.Cells[3, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            sheet.Cells[3, 1].Style.Font.Bold = true; // Negrita

            var salesData = await _productoRepository.GetHighlightedProducto(startDate, endDate);

            // ENCABEZADO DE TABLA
            sheet.Cells[5, 1].Value = "Producto";
            sheet.Cells[5, 2].Value = "Total Ventas";

            using (var range = sheet.Cells[5, 1, 5, 2])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.MediumSlateBlue);
                range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
            }

            for (int i = 0; i < salesData.Count; i++)
            {
                sheet.Cells[i + 6, 1].Value = salesData[i].Producto;
                sheet.Cells[i + 6, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                sheet.Cells[i + 6, 2].Value = salesData[i].Total;
                sheet.Cells[i + 6, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                sheet.Cells[i + 6, 2].Style.Numberformat.Format = "\"$\"#,##0.00";
            }

            sheet.Column(1).Width = 30;
            sheet.Column(2).Width = 20;

            var chart = sheet.Drawings.AddChart("CategoriasDestacadasChart", eChartType.BarClustered);
            chart.Title.Text = "Productos Más Destacados";
            chart.SetPosition(1, 0, 3, 0);
            chart.SetSize(800, 400);

            string valores = sheet.Cells[6, 2, salesData.Count + 5, 2].Address;
            string categoriasRange = sheet.Cells[6, 1, salesData.Count + 5, 1].Address;

            var serie = chart.Series.Add(valores, categoriasRange);
            serie.Header = "Total Ventas";
        }

        private void GenerateCoverSheet(ExcelWorksheet sheet, DateTime startDate, DateTime endDate, ExcelWorkbook workbook)
        {
            sheet.Cells.Style.Font.Name = "Calibri";
            sheet.Cells.Style.Font.Size = 12;

            sheet.Cells[2, 2].Value = "📊 Reporte de Productos";
            sheet.Cells[2, 2].Style.Font.Size = 20;
            sheet.Cells[2, 2].Style.Font.Bold = true;

            sheet.Cells[3, 2].Value = "Sistema: Academia Astrid Analytics";
            sheet.Cells[5, 2].Value = $"Desde: {startDate:dd/MM/yyyy}";
            sheet.Cells[6, 2].Value = $"Hasta: {endDate:dd/MM/yyyy}";

            // ✅ 2. Hora local (usamos hora de México como ejemplo, podés cambiarla)
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");
            var localTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
            sheet.Cells[7, 2].Value = $"Generado el: {localTime:dd/MM/yyyy HH:mm}";

            sheet.Cells[9, 2].Value = "Ir a las secciones:";

            // ✅ 1. Usamos los nombres reales del workbook
            sheet.Cells[10, 2].Hyperlink = new ExcelHyperLink($"'{workbook.Worksheets[0].Name}'!A1", "📈 Evolución por Producto y Fechas");
            sheet.Cells[11, 2].Hyperlink = new ExcelHyperLink($"'{workbook.Worksheets[1].Name}'!A1", "📊 Ventas por Producto");
            sheet.Cells[12, 2].Hyperlink = new ExcelHyperLink($"'{workbook.Worksheets[2].Name}'!A1", "⭐ Productos más Destacados");

            // Estilo
            sheet.Cells[2, 2, 2, 5].Merge = true;
            sheet.Cells[2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

            sheet.Column(2).Width = 40;
            sheet.View.ShowGridLines = false;
        }
    }
}
