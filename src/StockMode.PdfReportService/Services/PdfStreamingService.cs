
using Grpc.Core;
using StockMode.Contracts.Protos;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace StockMode.PdfReportService.Services
{
    public class PdfStreamingService : PdfGenerator.PdfGeneratorBase
    {
        public override Task GenerateTablePdf(PdfTableRequest request, IServerStreamWriter<PdfChunk> responseStream, ServerCallContext context)
        {
            return base.GenerateTablePdf(request, responseStream, context);
        }
    }
}
