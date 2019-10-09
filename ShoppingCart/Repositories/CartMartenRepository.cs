using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marten;
using ShoppingCart.Models;

namespace ShoppingCart.Repositories
{
    public class CartMartenRepository : ICartRepository
    {
        private readonly IDocumentStore documentStore;

        public CartMartenRepository(IDocumentStore documentStore)
        {
            this.documentStore = documentStore;
        }

        public async Task<Cart> AddProductsToCart(int userId, List<Product> products)
        {
            using (var session = documentStore.LightweightSession())
            {
                var cart = await session.LoadAsync<Models.Cart>(userId) ?? new Models.Cart { Id = userId, Products = new List<Product>() };
                cart.Products.AddRange(products);
                session.Store(cart);
                await session.SaveChangesAsync();


                return cart;
            }
        }

        public async Task<Cart> GetCart(int userId)
        {
            using (var session = documentStore.LightweightSession())
            {
                return await session.LoadAsync<Models.Cart>(userId);
            }
        }

        public async Task<Cart> RemoveProductFromCart(int userId, int productId)
        {
            using (var session = documentStore.LightweightSession())
            {
                Models.Cart cart = await session.LoadAsync<Models.Cart>(userId);
                if (cart == null)
                {
                    return null;
                }

                var productToRemove = cart.Products.SingleOrDefault(_ => _.Id == productId);
                if (productToRemove == null)
                {
                    return null;
                }

                cart.Products.Remove(productToRemove);
                session.Store(cart);
                await session.SaveChangesAsync();


                return cart;
            }
        }
    }
}
