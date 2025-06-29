using MediatR;

namespace StockMode.Domain.Core.Events
{
    public class Event : Message, INotification
    {
        public DateTime TimeStamp { get; set; }

        public Event()
        {
            TimeStamp = DateTime.UtcNow;
        }
    }
}
