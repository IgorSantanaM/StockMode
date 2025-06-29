using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Domain.Core.Events
{
    public class Event : Message
    {
        public DateTime TimeStamp { get; set; }

        public Event()
        {
            TimeStamp = DateTime.UtcNow;
        }
    }
}
