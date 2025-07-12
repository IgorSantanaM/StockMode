using MediatR;

namespace StockMode.Domain.Core.Events
{
    public abstract record Event : Message, INotification
    {
        public DateTime TimeStamp { get; init; }

        public Event()
        {
            TimeStamp = DateTime.UtcNow;
        }
    }
}
