using System;

namespace ShoppingCart.Events
{
    public abstract class Event
    {
        public Guid Id { get; }

        public DateTimeOffset OccuredAt { get; }
        public string Name { get; protected set; }

        public Event(Guid id, DateTimeOffset occuredAt)
        {
            Id = id;
            OccuredAt = occuredAt;
        }
    }
}
