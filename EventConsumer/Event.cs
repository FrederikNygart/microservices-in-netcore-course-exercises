using System;
using System.Collections.Generic;
using System.Text;

namespace EventConsumer
{
    public class Event
    {
        public int ShoppingCartId { get; set; }
        public int ProductId { get; set; }

        public Guid Id { get; set; }

        public DateTimeOffset OccuredAt { get; set; }
        public string Name { get; set; }
    }
}
