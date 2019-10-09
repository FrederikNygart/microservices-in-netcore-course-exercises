using Nancy;
using System;

namespace ShoppingCart.Modules
{
    public class EventFeed : NancyModule
    {
        public EventFeed(EventStore eventStore) : base("shoppingCart/events")
        {
            Get("/", _ =>
            {
                if (!DateTimeOffset.TryParse(Request.Query.from.Value, out DateTimeOffset from))
                    from = DateTimeOffset.Now.AddHours(-1);

                if (!DateTimeOffset.TryParse(this.Request.Query.to.Value, out DateTimeOffset to))
                    to = DateTimeOffset.Now;

                return eventStore.GetEvents(from, to);
            });
        }
    }
}