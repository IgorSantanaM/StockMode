using QuestPDF.Fluent;

namespace StockMode.PdfReportService.Interfaces
{
    public interface IPdfMaker
    {
        byte[] CreatePdfGeneric<TModel>(TModel data, Action<RowDescriptor, TModel> renderAction);
    }
}
