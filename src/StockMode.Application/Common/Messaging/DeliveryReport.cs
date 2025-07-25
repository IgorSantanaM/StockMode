using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Common.Messaging
{
    public record DeliveryReport
    {
        public Guid MessageId { get; init; }

        public string Recipient { get; init; }

        public DeliveryStatus Status { get; init; }

        public string? ErrorDetails { get; init; }

        public IReadOnlyDictionary<string, object> Metadata { get; init; } = new Dictionary<string, object>();
    }
}
