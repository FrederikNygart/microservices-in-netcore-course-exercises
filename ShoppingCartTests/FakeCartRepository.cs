using Marten;
using ShoppingCart.Models;
using ShoppingCart.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartTests
{
    public class FakeCartRepository : ICartRepository
    {
        public ICollection<Cart> cartlist { get; set; }

        public FakeCartRepository()
        {
            cartlist = new List<Cart>();
            cartlist.Add(new Cart { Id = 123, Products = new List<Product>() });

        }

        public async Task<Cart> AddProductsToCart(int userId, List<Product> products)
        {
            var cart = cartlist.SingleOrDefault(x => x.Id == userId);
            cart.Products.AddRange(products);
            return cart;
        }

        public Task<Cart> GetCart(int userId)
        {
            var cart = cartlist.SingleOrDefault(x => x.Id == userId);
            return Task.FromResult<Cart>(cart);
        }

        public Task<Cart> RemoveProductFromCart(int userId, int productId)
        {
            var cart = cartlist.SingleOrDefault(x => x.Id == userId);
            var product = cart.Products.SingleOrDefault(x => x.Id == productId);
            cart.Products.Remove(product);
            return Task.FromResult<Cart>(cart);
        }
    }
}
