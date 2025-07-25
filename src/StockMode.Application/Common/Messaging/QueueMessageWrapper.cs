using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Common.Messaging
{
    public record QueueMessageWrapper
    {
        public string MessageType { get; init; }
        public string Payload { get; init; }
    }
}
