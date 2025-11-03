using StockMode.Application.Common.Interfaces;
using StockMode.Application.Common.Messaging;

namespace StockMode.Infra.Services.Email
{
    public class SignalRDeliveryReporter : IMessageDeliveryReporter
    {
        public Task ReportAsync(DeliveryReport report)
        {
            Console.WriteLine("Not Implemented!");
            return Task.CompletedTask;
        }
    }
}
