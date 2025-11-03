using QuestPDF.Fluent;

namespace StockMode.Application.Common.Interfaces
{
    public interface IPdfMaker
    {
        byte[] CreatePdfGeneric<TModel>(TModel data, Action<RowDescriptor, TModel> renderAction);
    }
}
