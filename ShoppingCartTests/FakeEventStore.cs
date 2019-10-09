using ShoppingCart;
using ShoppingCart.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCartTests
{
    public class FakeEventStore : IEventStore
    {
        private List<object> store;

        public FakeEventStore()
        {
            this.store = new List<object>();
        }

        public IEnumerable<Event> GetEvents(DateTimeOffset from, DateTimeOffset to)
        {
            return store.Select(x => x as Event);
        }

        public void Raise<T>(T @event)
        {
            store.Add(@event);
        }
    }
}
