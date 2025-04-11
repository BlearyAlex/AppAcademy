using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.ControlVentas;
using AppAcademy.Domain.PuntoDeVenta;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace AppAcademy.Services
{
    public class ReciboAbonoPdfService : IReciboAbonoPdfService
    {
        public byte[] GenerarReciboAbonoPDF(Venta venta, Abono abono)
        {
            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A5);
                    page.Margin(30);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12).FontFamily("Arial"));

                    page.Header().Row(row =>
                    {
                        row.RelativeColumn().Column(col =>
                        {
                            col.Item().Text("Nombre del Negocio").Bold().FontSize(16);
                            col.Item().Text("Dirección del negocio");
                            col.Item().Text("Tel: 555-123-4567");
                        });

                        row.ConstantColumn(100).Height(50).AlignRight().Image("wwwroot/logo.png", ImageScaling.FitArea); // Opcional
                    });

                    page.Content().Column(col =>
                    {
                        col.Spacing(10);

                        col.Item().Text($"📅 Fecha: {abono.Fecha.ToString("dd/MM/yyyy")}").Bold();
                        col.Item().Text($"👤 Cliente: {venta.Cliente?.Nombre + venta.Cliente.Apellido ?? "Sin nombre"}");
                        col.Item().Text($"📞 Teléfono: {venta.Cliente?.Telefono ?? "Sin teléfono"}");

                        col.Item().Text("🛒 Productos:").Bold();
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.ConstantColumn(60);
                                columns.ConstantColumn(80);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Producto").Bold();
                                header.Cell().Text("Cantidad").Bold();
                                header.Cell().Text("Precio").Bold();
                            });

                            foreach (var detalle in venta.DetalleVentas)
                            {
                                table.Cell().Text(detalle.Producto.Nombre);
                                table.Cell().Text(detalle.Cantidad.ToString());
                                table.Cell().Text($"{detalle.PrecioUnitario:C}");
                            }
                        });

                        col.Item().Text($"💰 Total: {venta.Total:C}");
                        col.Item().Text($"💵 Abono recibido: {abono.Monto:C}");
                        col.Item().Text($"💳 Saldo pendiente: {venta.SaldoPendiente:C}");
                        col.Item().Text($"📌 Estado: {venta.EstadoVenta}").Bold();
                    });

                    page.Footer().AlignCenter().Text("Gracias por su pago ❤️").FontSize(10).Italic();
                });
            });

            return pdf.GeneratePdf();
        }
    }
}
