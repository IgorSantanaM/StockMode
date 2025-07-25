using StockMode.Application.Common.Messaging;

namespace StockMode.Application.Common.Interfaces;

public interface IMessageDeliveryReporter
{
    Task ReportReportAsync(DeliveryReport report);
}
