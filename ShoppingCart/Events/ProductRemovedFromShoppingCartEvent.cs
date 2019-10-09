using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Events
{
    public class ProductRemovedFromShoppingCartEvent : Event
    {
        public int ShoppingCartId { get; }
        public int ProductId { get; }

        public ProductRemovedFromShoppingCartEvent(Guid id, DateTimeOffset occuredAt, int shoppingCartId, int productId) :
            base(id, occuredAt)
        {
            this.Name = nameof(ProductRemovedFromShoppingCartEvent);
            this.ShoppingCartId = shoppingCartId;
            this.ProductId = productId;
        }
    }
}
