using System;

namespace ShoppingCart.Events
{
    public class ProductAddedToShoppingCartEvent : Event
    {
        public int ShoppingCartId { get;}
        public int ProductId { get; }

        public ProductAddedToShoppingCartEvent(Guid id, DateTimeOffset occuredAt, int shoppingCartId, int productId) : 
            base(id, occuredAt)
        {
            this.Name = nameof(ProductAddedToShoppingCartEvent);
            this.ShoppingCartId = shoppingCartId;
            this.ProductId = productId;
        }
    }
}
