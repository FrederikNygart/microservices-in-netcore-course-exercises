using Marten;
using ShoppingCart.Events;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart
{
    public class EventStore : IEventStore
    {
        private readonly IDocumentStore documentStore;

        public EventStore(IDocumentStore documentStore)
        {
            this.documentStore = documentStore;
        }

        public void Raise<T>(T @event)
        {
            using (var session = documentStore.LightweightSession())
            {
                session.Store(@event);
                session.SaveChanges();
            }
        }


        public IEnumerable<Event> GetEvents(DateTimeOffset from, DateTimeOffset to)
        {
            using (var session = documentStore.LightweightSession())
            {
                var events = new List<Event>();

                events.AddRange(
                    session.Query<ProductAddedToShoppingCartEvent>()
                        .Where(e => e.OccuredAt >= from && e.OccuredAt <= to));

                events.AddRange(
                    session.Query<ProductRemovedFromShoppingCartEvent>()
                        .Where(e => e.OccuredAt >= from && e.OccuredAt <= to));

                return events.OrderBy(e => e.OccuredAt);
            }
        }
    }
}
