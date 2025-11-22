using QRCoder;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using StockMode.Application.Features.Sales.Dtos;
using StockMode.Application.Helpers;
using System.Globalization;

namespace StockMode.Application.PDF
{
    public static class SaleCompletedRenderer
    {
        private static readonly SvgImage StockModeLogoTypeSvg 
            = SvgImage.FromText(EmbeddedResource.ReadAllText("stockmode-logo.svg"));

        private static readonly string PrimaryBlue = "#1E40AF";
        private static readonly string SecondaryBlue = "#3B82F6";
        private static readonly string LightBlue = "#EBF8FF";
        private static readonly string DarkGray = "#374151";
        private static readonly string MediumGray = "#6B7280";
        private static readonly string LightGray = "#F3F4F6";
        private static readonly string White = "#FFFFFF";
        private static readonly string BorderGray = "#E5E7EB";
        private static readonly string SuccessGreen = "#10B981";
        public static void DrawSaleCompletedDetails(RowDescriptor row, SaleCompletedEmail model)
        {
            row.RelativeItem().Column(column =>
            {
                column.Spacing(15);

                column.Item().Element(c => ComposeHeader(c, model));
                column.Item().Element(c => ComposeSaleInfoCard(c, model));
                column.Item().Element(c => ComposeItemsTable(c, model));
                column.Item().Element(c => ComposeSummarySection(c, model));
                column.Item().Element(ComposeFooter);
            });
        }

        private static void ComposeHeader(IContainer container, SaleCompletedEmail model)
        {
            container.Background(LightBlue).Padding(20, Unit.Point).Row(row =>
            {
                // Logo and company name
                row.RelativeItem().Column(column =>
                {
                    column.Item().Width(140, Unit.Point).Svg(StockModeLogoTypeSvg);
                    column.Item().PaddingTop(8, Unit.Point).Text("StockMode Inc.")
                        .FontSize(11)
                        .FontColor(MediumGray)
                        .Medium();
                });

                // Receipt title and sale ID
                row.RelativeItem().AlignRight().Column(column =>
                {
                    column.Item().AlignRight().Text("SALE RECEIPT")
                        .FontSize(24)
                        .FontColor(PrimaryBlue)
                        .Bold();
                    
                    column.Item().PaddingTop(5, Unit.Point).AlignRight().Text($"#{model.SaleId:D6}")
                        .FontSize(14)
                        .FontColor(SecondaryBlue)
                        .SemiBold();
                });
            });
        }

        private static void ComposeSaleInfoCard(IContainer container, SaleCompletedEmail model)
        {
            container.Border(1, Unit.Point).BorderColor(BorderGray).Background(Colors.White).Padding(15, Unit.Point).Column(column =>
            {
                column.Spacing(12, Unit.Point);

                // Customer information
                column.Item().Column(infoColumn =>
                {
                    infoColumn.Item().Text("CUSTOMER INFORMATION")
                        .FontSize(9)
                        .FontColor(MediumGray)
                        .Bold()
                        .LetterSpacing(0.5f);

                    infoColumn.Item().PaddingTop(5, Unit.Point).Row(infoRow =>
                    {
                        infoRow.ConstantItem(60, Unit.Point).Text("Email:")
                            .FontSize(10)
                            .FontColor(DarkGray)
                            .SemiBold();
                        infoRow.RelativeItem().Text(model.Email)
                            .FontSize(10)
                            .FontColor(DarkGray);
                    });
                });

                // Sale date and status
                column.Item().Column(detailsColumn =>
                {
                    detailsColumn.Item().Text("TRANSACTION DETAILS")
                        .FontSize(9)
                        .FontColor(MediumGray)
                        .Bold()
                        .LetterSpacing(0.5f);

                    detailsColumn.Item().PaddingTop(5, Unit.Point).Row(infoRow =>
                    {
                        infoRow.ConstantItem(60, Unit.Point).Text("Date:")
                            .FontSize(10)
                            .FontColor(DarkGray)
                            .SemiBold();
                        infoRow.RelativeItem().Text(model.SaleDate.ToString("dd/MM/yyyy HH:mm", CultureInfo.CurrentCulture))
                            .FontSize(10)
                            .FontColor(DarkGray);
                    });

                    detailsColumn.Item().PaddingTop(5, Unit.Point).Row(infoRow =>
                    {
                        infoRow.ConstantItem(60, Unit.Point).Text("Status:")
                            .FontSize(10)
                            .FontColor(DarkGray)
                            .SemiBold();
                        infoRow.AutoItem().Background(SuccessGreen)
                            .PaddingVertical(4, Unit.Point)
                            .PaddingHorizontal(8, Unit.Point)
                            .AlignCenter()
                            .Text("COMPLETED")
                            .FontSize(8)
                            .FontColor(Colors.White)
                            .Bold();
                    });
                });
            });
        }

        private static void ComposeItemsTable(IContainer container, SaleCompletedEmail model)
        {
            container.Column(column =>
            {
                // Section header with accent line
                column.Item().Row(titleRow =>
                {
                    titleRow.RelativeItem().Height(3, Unit.Point).Background(PrimaryBlue);
                });

                column.Item().PaddingTop(10, Unit.Point).PaddingBottom(10, Unit.Point).Text("Items Purchased")
                    .FontSize(14)
                    .FontColor(PrimaryBlue)
                    .Bold();

                // Table
                column.Item().Border(1, Unit.Point).BorderColor(BorderGray).Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(4);    // Product name
                        columns.RelativeColumn(1.5f); // Quantity
                        columns.RelativeColumn(2);    // Unit price
                        columns.RelativeColumn(2);    // Subtotal
                    });

                    // Table header
                    table.Header(header =>
                    {
                        header.Cell().Background(PrimaryBlue)
                            .Padding(8, Unit.Point)
                            .Text("Product")
                            .FontSize(9)
                            .FontColor(Colors.White)
                            .Bold();

                        header.Cell().Background(PrimaryBlue)
                            .Padding(8, Unit.Point)
                            .AlignCenter()
                            .Text("Qty")
                            .FontSize(9)
                            .FontColor(Colors.White)
                            .Bold();

                        header.Cell().Background(PrimaryBlue)
                            .Padding(8, Unit.Point)
                            .AlignRight()
                            .Text("Unit Price")
                            .FontSize(9)
                            .FontColor(Colors.White)
                            .Bold();

                        header.Cell().Background(PrimaryBlue)
                            .Padding(8, Unit.Point)
                            .AlignRight()
                            .Text("Subtotal")
                            .FontSize(9)
                            .FontColor(Colors.White)
                            .Bold();
                    });

                    // Table rows with alternating colors
                    var items = model.Items.ToList();
                    for (int i = 0; i < items.Count; i++)
                    {
                        var item = items[i];
                        var isEven = i % 2 == 0;
                        var bgColor = isEven ? White : LightGray;
                        var itemTotal = item.Quantity * item.PriceAtSale;

                        table.Cell().Background(bgColor)
                            .Padding(8, Unit.Point)
                            .BorderBottom(1, Unit.Point)
                            .BorderColor(BorderGray)
                            .Text(item.Name)
                            .FontSize(9)
                            .FontColor(DarkGray);

                        table.Cell().Background(bgColor)
                            .Padding(8, Unit.Point)
                            .BorderBottom(1, Unit.Point)
                            .BorderColor(BorderGray)
                            .AlignCenter()
                            .Text(item.Quantity.ToString())
                            .FontSize(9)
                            .FontColor(DarkGray)
                            .Bold();

                        table.Cell().Background(bgColor)
                            .Padding(8, Unit.Point)
                            .BorderBottom(1, Unit.Point)
                            .BorderColor(BorderGray)
                            .AlignRight()
                            .Text(item.PriceAtSale.ToString("C", CultureInfo.CurrentCulture))
                            .FontSize(9)
                            .FontColor(DarkGray);

                        table.Cell().Background(bgColor)
                            .Padding(8, Unit.Point)
                            .BorderBottom(1, Unit.Point)
                            .BorderColor(BorderGray)
                            .AlignRight()
                            .Text(itemTotal.ToString("C", CultureInfo.CurrentCulture))
                            .FontSize(9)
                            .FontColor(DarkGray)
                            .SemiBold();
                    }
                });
            });
        }

        private static void ComposeSummarySection(IContainer container, SaleCompletedEmail model)
        {
            container.PaddingTop(10, Unit.Point).Column(summaryColumn =>
            {
                summaryColumn.Spacing(10, Unit.Point);

                // Payment method info (simple row)
                summaryColumn.Item().Border(1, Unit.Point).BorderColor(BorderGray)
                    .Background(LightGray)
                    .Padding(12, Unit.Point)
                    .Column(paymentColumn =>
                    {
                        paymentColumn.Item().Text("Payment Method")
                            .FontSize(9)
                            .FontColor(MediumGray)
                            .Bold();

                        paymentColumn.Item().PaddingTop(4, Unit.Point).Text(GetPaymentMethodDisplayName(model.PaymentMethod))
                            .FontSize(11)
                            .FontColor(DarkGray)
                            .SemiBold();
                    });

                // Financial summary with proper spacing
                summaryColumn.Item().Column(financeColumn =>
                {
                    financeColumn.Spacing(6, Unit.Point);

                    // Subtotal
                    financeColumn.Item().Row(row =>
                    {
                        row.RelativeItem().Text("Subtotal:")
                            .FontSize(11)
                            .FontColor(MediumGray)
                            .SemiBold();
                        row.AutoItem().Text(model.TotalPrice.ToString("C", CultureInfo.CurrentCulture))
                            .FontSize(11)
                            .FontColor(DarkGray);
                    });

                    // Discount (if applicable)
                    if (model.Discount > 0)
                    {
                        financeColumn.Item().Row(row =>
                        {
                            row.RelativeItem().Text("Discount:")
                                .FontSize(11)
                                .FontColor(SuccessGreen)
                                .SemiBold();
                            row.AutoItem().Text($"- {model.Discount.ToString("C", CultureInfo.CurrentCulture)}")
                                .FontSize(11)
                                .FontColor(SuccessGreen)
                                .SemiBold();
                        });
                    }

                    // Divider
                    financeColumn.Item().PaddingVertical(6, Unit.Point).LineHorizontal(2, Unit.Point).LineColor(PrimaryBlue);

                    // Total (highlighted box)
                    financeColumn.Item().Background(PrimaryBlue)
                        .Padding(12, Unit.Point)
                        .Row(row =>
                        {
                            row.RelativeItem().Text("TOTAL:")
                                .FontSize(14)
                                .FontColor(Colors.White)
                                .Bold();
                            row.AutoItem().Text(model.FinalPrice.ToString("C", CultureInfo.CurrentCulture))
                                .FontSize(16)
                                .FontColor(Colors.White)
                                .Bold();
                        });
                });
            });
        }

        private static void ComposeFooter(IContainer container)
        {
            container.PaddingTop(20, Unit.Point).Column(column =>
            {
                column.Item().Height(2, Unit.Point).Background(LightBlue);

                column.Item().PaddingTop(15, Unit.Point).AlignCenter().Column(footerColumn =>
                {
                    footerColumn.Item().Text("Thank you for choosing StockMode!")
                        .FontSize(12)
                        .FontColor(PrimaryBlue)
                        .Bold();

                    footerColumn.Item().PaddingTop(8, Unit.Point).Text("Your satisfaction is our priority")
                        .FontSize(9)
                        .FontColor(MediumGray)
                        .Italic();

                    footerColumn.Item().PaddingTop(12, Unit.Point).Text(text =>
                    {
                        text.Span("For support: ").FontSize(8).FontColor(MediumGray);
                        text.Span("support@stockmode.com").FontSize(8).FontColor(SecondaryBlue).SemiBold();
                    });

                    footerColumn.Item().PaddingTop(4, Unit.Point).Text("StockMode Inc. | www.stockmode.com | +1 (555) 123-4567")
                        .FontSize(7)
                        .FontColor(MediumGray);
                });
            });
        }

        private static string GetPaymentMethodDisplayName(StockMode.Domain.Enums.PaymentMethod paymentMethod)
        {
            return paymentMethod switch
            {
                StockMode.Domain.Enums.PaymentMethod.Pix => "PIX",
                StockMode.Domain.Enums.PaymentMethod.CreditCard => "Credit Card",
                StockMode.Domain.Enums.PaymentMethod.DebitCard => "Debit Card",
                StockMode.Domain.Enums.PaymentMethod.Cash => "Cash",
                StockMode.Domain.Enums.PaymentMethod.StoreCredit => "Store Credit",
                _ => "Other"
            };
        }
    }
}
