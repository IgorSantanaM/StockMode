using QRCoder;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using StockMode.Application.Common.Interfaces;
using StockMode.Application.Helpers;

namespace StockMode.Infra.Services.PDF
{
    public class PdfMaker : IPdfMaker
    {
        static PdfMaker() => 
            QuestPDF.Settings.License = LicenseType.Community;

        private void DrawDetails<TModel>(RowDescriptor row, TModel model, Action<RowDescriptor, TModel> renderAction) =>
            renderAction.Invoke(row, model);
        public byte[] CreatePdfGeneric<TModel>(TModel data, Action<RowDescriptor, TModel> renderAction)
        {
            var now = DateTimeOffset.UtcNow;
            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.DefaultTextStyle(text => text.FontFamily("PT Sans").FontSize(14));
                    page.Margin(1, Unit.Centimetre);
                    page.Content().Column(column =>
                    {
                        column.Item().Row(row =>
                        {
                            DrawDetails(row, data, renderAction);
                        });
                    });
                });
            }).WithMetadata(new DocumentMetadata
            {
                CreationDate = now,
                ModifiedDate = now
            });
            return pdf.GeneratePdf();
        }
    }
}
