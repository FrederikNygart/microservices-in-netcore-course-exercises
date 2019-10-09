using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marten;
using Nancy;
using Nancy.ModelBinding;
using ShoppingCart.Events;
using ShoppingCart.Models;
using ShoppingCart.Repositories;

namespace ShoppingCart.Modules
{
    public class Cart : NancyModule
    {
        private readonly ICartRepository documentStore;
        private readonly IEventStore eventStore;

        public Cart(ICartRepository documentStore, IEventStore eventStore) : base("/shoppingcart")
        {
            this.documentStore = documentStore;
            this.eventStore = eventStore;

            base.Get("/{userId:int}", async parameters =>
            {
                var cart = await GetCart(parameters.userId.Value);
                return cart ?? (object)HttpStatusCode.NotFound;
            });

            Post("/{userId:int}", async parameters =>
            {
                int userId = parameters.userId;
                List<Product> products = this.Bind();

                return await AddProductsToCart(userId, products);
            });

            Delete("/{userId:int}/items/{productId:int}", async parameters => 
            {
                int userId = parameters.userId;
                int productId = parameters.productId;

                return await RemoveProductFromCart(userId, productId);
            });
        }

        private async Task<Models.Cart> GetCart(int userId)
        {
            return await documentStore.GetCart(userId);
        }

        private async Task<Models.Cart> AddProductsToCart(int userId, List<Product> products)
        {

            return await documentStore.AddProductsToCart(userId, products);
            //using (var session = documentStore.LightweightSession())
            //{
            //    var cart = await session.LoadAsync<Models.Cart>(userId) ?? new Models.Cart { Id = userId, Products = new List<Product>() };
            //    cart.Products.AddRange(products);
            //    session.Store(cart);
            //    await session.SaveChangesAsync();

            //    foreach (var product in products)
            //    {
            //        eventStore.Raise(
            //            new ProductAddedToShoppingCartEvent(Guid.NewGuid(), DateTimeOffset.Now, userId, product.Id));
            //    }

            //    return cart;
            //}
        }

        private async Task<HttpStatusCode> RemoveProductFromCart(int userId, int productId)
        {

            await documentStore.RemoveProductFromCart(userId, productId);
            return HttpStatusCode.NoResponse;

            //eventStore.Raise(
            //    new ProductRemovedFromShoppingCartEvent(Guid.NewGuid(), DateTimeOffset.Now, userId, productId));
        }
    }
}