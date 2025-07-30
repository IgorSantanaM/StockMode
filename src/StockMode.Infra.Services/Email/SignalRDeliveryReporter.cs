using StockMode.Application.Common.Interfaces;
using StockMode.Application.Common.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Infra.Services.Email
{
    public class SignalRDeliveryReporter : IMessageDeliveryReporter
    {
        public Task ReportReportAsync(DeliveryReport report)
        {
            throw new NotImplementedException();
        }
    }
}
