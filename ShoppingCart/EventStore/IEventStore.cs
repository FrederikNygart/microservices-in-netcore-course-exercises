using System;
using System.Collections.Generic;
using ShoppingCart.Events;

namespace ShoppingCart
{
    public interface IEventStore
    {
        IEnumerable<Event> GetEvents(DateTimeOffset from, DateTimeOffset to);
        void Raise<T>(T @event);
    }
}