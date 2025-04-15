using AppAcademy.Application.Contracts.Persistence;
using AppAcademy.Domain.ControlAcademia;
using AppAcademy.Domain.ControlVentas;
using AppAcademy.Domain.PuntoDeVenta;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace AppAcademy.Services
{
    public class ReciboAbonoPdfService : IReciboAbonoPdfService
    {
        public byte[] GenerarReciboAbonoAcademyPDF(Payment payment, AbonoAcademy abono, decimal cambio)
        {
            var fechaUtc = DateTime.SpecifyKind(abono.FechaAbono, DateTimeKind.Utc);
            var fechaLocal = fechaUtc.ToLocalTime();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A5);
                    page.Margin(30);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11).FontFamily("Arial"));

                    // Encabezado con logo y datos
                    page.Header().Row(row =>
                    {
                        row.ConstantColumn(60).Height(60).Image("wwwroot/logo.png", ImageScaling.FitArea);

                        row.RelativeColumn().PaddingLeft(10).AlignMiddle().Column(col =>
                        {
                            col.Item().Text("Academia Astrid").Bold().FontSize(16);
                            col.Item().Text("Dirección del negocio").FontSize(10);
                            col.Item().Text("Tel: 555-123-4567").FontSize(10);
                            col.Item().Text("academia@gmail.com").FontSize(10);
                        });

                        row.ConstantColumn(120).AlignRight().Column(col =>
                        {
                            col.Item().Border(1).Padding(5).Column(innerCol =>
                            {
                                innerCol.Item().Text("RUC 12345678900").FontSize(10).AlignCenter();
                                innerCol.Item().Text("Recibo de Abono").Bold().FontSize(12).AlignCenter().FontColor(Colors.Blue.Medium);
                                innerCol.Item().Text($"N° {abono.AbonoAcademyId}").FontSize(10).AlignCenter();
                            });
                        });
                    });

                    page.Content().Column(col =>
                    {
                        col.Spacing(5);

                        // Información del cliente
                        col.Item().Text($"Fecha: {fechaLocal:dd/MM/yyyy}");
                        col.Item().Text($"Estudiante: {payment.Student?.Nombre + " " + payment.Student?.Apellido ?? "Sin Estudiante"}");
                        col.Item().Text($"Teléfono: {payment.Student?.Telefono ?? "Sin teléfono"}");

                        // Tabla de productos
                        col.Item().PaddingTop(10).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(70); // Carrera
                                columns.RelativeColumn();   // Ciclo Academico
                                columns.ConstantColumn(70); // Mes
                                columns.ConstantColumn(70); // Total
                            });

                            // Encabezado con fondo de color
                            table.Header(header =>
                            {
                                header.Cell().Background(Colors.Grey.Lighten2).Text("CARRERA.").Bold();
                                header.Cell().Background(Colors.Grey.Lighten2).Text("CICLO ACADEMICO").Bold();
                                header.Cell().Background(Colors.Grey.Lighten2).AlignRight().Text("MES").Bold();
                                header.Cell().Background(Colors.Grey.Lighten2).AlignRight().Text("TOTAL").Bold();
                            });

                                table.Cell().Element(CellStyle).Text(payment.Career?.Nombre ?? "Sin Carrera" );
                                table.Cell().Element(CellStyle).Text(payment.Career?.AcademicCycles.FirstOrDefault()?.CicloAcademico ?? "Sin Ciclo");
                                table.Cell().Element(CellStyle).AlignRight().Text(payment.MesPagado);
                                table.Cell().Element(CellStyle).AlignRight().Text($"${payment.Total:0.00}");
                        });

                        col.Item().AlignRight().Column(rightCol =>
                        {
                            rightCol.Item().Text($"Total: ${payment.Total}").Bold();
                            rightCol.Item().Text($"Descuento: %{payment.Descuento}");
                            rightCol.Item().Text($"Su Pago: ${abono.Monto}");
                            rightCol.Item().Text($"Cambio: ${cambio:0.00}").FontColor(Colors.Green.Medium).Bold();
                            rightCol.Item().Text($"Saldo pendiente: ${payment.SaldoPendiente}");
                            rightCol.Item().Text($"Estado: {payment.EstadoVenta}").Bold();
                        });

                        // Nota adicional
                        col.Item().PaddingTop(15).Border(1).Background(Colors.Grey.Lighten3).Padding(5)
                            .Text("Nota Adicional").Bold();
                    });

                    // Pie de página
                    page.Footer().AlignCenter().Text("Gracias por su pago").FontSize(10).Italic();
                });
            });

            return pdf.GeneratePdf();

            IContainer CellStyle(IContainer container)
            {
                return container.BorderBottom(1).PaddingVertical(5).PaddingHorizontal(2);
            }
        }

        public byte[] GenerarReciboAbonoPDF(Venta venta, Abono abono, decimal cambio)
        {
            var fechaUtc = DateTime.SpecifyKind(abono.Fecha, DateTimeKind.Utc);
            var fechaLocal = fechaUtc.ToLocalTime();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A5);
                    page.Margin(30);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11).FontFamily("Arial"));

                    // Encabezado con logo y datos
                    page.Header().Row(row =>
                    {
                        row.ConstantColumn(60).Height(60).Image("wwwroot/logo.png", ImageScaling.FitArea);

                        row.RelativeColumn().PaddingLeft(10).AlignMiddle().Column(col =>
                        {
                            col.Item().Text("Academia Astrid").Bold().FontSize(16);
                            col.Item().Text("Dirección del negocio").FontSize(10);
                            col.Item().Text("Tel: 555-123-4567").FontSize(10);
                            col.Item().Text("academia@gmail.com").FontSize(10);
                        });

                        row.ConstantColumn(120).AlignRight().Column(col =>
                        {
                            col.Item().Border(1).Padding(5).Column(innerCol =>
                            {
                                innerCol.Item().Text("RUC 12345678900").FontSize(10).AlignCenter();
                                innerCol.Item().Text("Recibo de Abono").Bold().FontSize(12).AlignCenter().FontColor(Colors.Blue.Medium);
                                innerCol.Item().Text($"N° {abono.AbonoId}").FontSize(10).AlignCenter();
                            });
                        });
                    });

                    page.Content().Column(col =>
                    {
                        col.Spacing(5);

                        // Información del cliente
                        col.Item().Text($"Fecha: {fechaLocal:dd/MM/yyyy}");
                        col.Item().Text($"Cliente: {venta.Cliente?.Nombre + " " + venta.Cliente?.Apellido ?? "Cliente Generico"}");
                        col.Item().Text($"Teléfono: {venta.Cliente?.Telefono ?? "Sin teléfono"}");

                        // Tabla de productos
                        col.Item().PaddingTop(10).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(40); // Cantidad
                                columns.RelativeColumn();   // Producto
                                columns.ConstantColumn(70); // P. Unitario
                                columns.ConstantColumn(70); // Total
                            });

                            // Encabezado con fondo de color
                            table.Header(header =>
                            {
                                header.Cell().Background(Colors.Grey.Lighten2).Text("CANT.").Bold();
                                header.Cell().Background(Colors.Grey.Lighten2).Text("PRODUCTO").Bold();
                                header.Cell().Background(Colors.Grey.Lighten2).AlignRight().Text("P.UNITARIO").Bold();
                                header.Cell().Background(Colors.Grey.Lighten2).AlignRight().Text("TOTAL").Bold();
                            });

                            foreach (var detalle in venta.DetalleVentas)
                            {
                                table.Cell().Element(CellStyle).Text(detalle.Cantidad.ToString());
                                table.Cell().Element(CellStyle).Text(detalle.Producto.Nombre);
                                table.Cell().Element(CellStyle).AlignRight().Text($"${detalle.PrecioUnitario}");
                                var totalProducto = detalle.Cantidad * detalle.PrecioUnitario;
                                table.Cell().Element(CellStyle).AlignRight().Text($"${totalProducto}");
                            }
                        });

                        col.Item().AlignRight().Column(rightCol =>
                        {
                            rightCol.Item().Text($"Total: ${venta.Total}").Bold();
                            rightCol.Item().Text($"Descuento: %{venta.Descuento}");
                            rightCol.Item().Text($"Impuesto: %{venta.Impuesto}");
                            rightCol.Item().Text($"Su Pago: ${abono.Monto}");
                            rightCol.Item().Text($"Cambio: ${cambio:0.00}").FontColor(Colors.Green.Medium).Bold();
                            rightCol.Item().Text($"Saldo pendiente: ${venta.SaldoPendiente}");
                            rightCol.Item().Text($"Estado: {venta.EstadoVenta}").Bold();
                        });

                        // Nota adicional
                        col.Item().PaddingTop(15).Border(1).Background(Colors.Grey.Lighten3).Padding(5)
                            .Text("Nota Adicional").Bold();
                    });

                    // Pie de página
                    page.Footer().AlignCenter().Text("Gracias por su pago").FontSize(10).Italic();
                });
            });

            return pdf.GeneratePdf();

            IContainer CellStyle(IContainer container)
            {
                return container.BorderBottom(1).PaddingVertical(5).PaddingHorizontal(2);
            }
        }
    }
}
