using QRCoder;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using StockMode.Application.Features.Sales.Dtos;
using System.Globalization;

namespace StockMode.Application.PDF
{
    public static class SaleCompletedRenderer
    {
        public static void DrawSaleCompletedDetails(RowDescriptor row, SaleCompletedEmail model)
        {
            row.ConstantItem(18, Unit.Centimetre).Column(column =>
            {
                column.Spacing(10);

                column.Item().Text("Sale Receipt")
                    .FontSize(24)
                    .Bold()
                    .AlignCenter();

                column.Item().Text(text =>
                {
                    text.Span("Sale ID: ").SemiBold();
                    text.Span(model.SaleId.ToString());
                    text.Span("   ");
                    text.Span("Date: ").SemiBold();
                    text.Span(model.SaleDate.ToString("g", CultureInfo.InvariantCulture));
                });

                column.Item().Text(text =>
                {
                    text.Span("Customer Email: ").SemiBold();
                    text.Span(model.Email);
                });

                column.Item().PaddingTop(10).Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(4);
                        columns.RelativeColumn(2);
                        columns.RelativeColumn(2);
                        columns.RelativeColumn(2);
                    });

                    table.Header(header =>
                    {
                        header.Cell().Element(CellStyle).Text("Item").Bold();
                        header.Cell().Element(CellStyle).Text("Qty").Bold();
                        header.Cell().Element(CellStyle).Text("Unit Price").Bold();
                        header.Cell().Element(CellStyle).Text("Total").Bold();
                    });

                    foreach (var item in model.Items)
                    {
                        table.Cell().Element(CellStyle).Text(item.Name);
                        table.Cell().Element(CellStyle).Text(item.Quantity.ToString());
                        table.Cell().Element(CellStyle).Text(item.PriceAtSale.ToString("C", CultureInfo.CurrentCulture));
                        table.Cell().Element(CellStyle).Text((item.Quantity * item.PriceAtSale).ToString("C", CultureInfo.CurrentCulture));
                    }

                    static IContainer CellStyle(IContainer container) =>
                        container.PaddingVertical(2).PaddingHorizontal(4);

                    column.Item().PaddingTop(10).AlignRight().Column(summary =>
                    {
                        summary.Item().Text(text =>
                        {
                            text.Span("Subtotal: ").SemiBold();
                            text.Span(model.TotalPrice.ToString("C", CultureInfo.CurrentCulture));
                        });
                        summary.Item().Text(text =>
                        {
                            text.Span("Discount: ").SemiBold();
                            text.Span(model.Discount.ToString("C", CultureInfo.CurrentCulture));
                        });
                        summary.Item().Text(text =>
                        {
                            text.Span("Final Price: ").Bold();
                            text.Span(model.FinalPrice.ToString("C", CultureInfo.CurrentCulture)).Bold();
                        });
                        summary.Item().Text(text =>
                        {
                            text.Span("Payment Method: ").SemiBold();
                            text.Span(model.PaymentMethod.ToString());
                        });
                    });
                });
            });
        }

    }
}
