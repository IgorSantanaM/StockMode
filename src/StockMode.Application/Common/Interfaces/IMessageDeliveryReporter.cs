using StockMode.Application.Common.Messaging;

namespace StockMode.Application.Common.Interfaces;

public interface IMessageDeliveryReporter
{
    Task ReportAsync(DeliveryReport report); // This method is used to report the delivery status of messages, such as emails or notifications, including success and failure cases.
}
