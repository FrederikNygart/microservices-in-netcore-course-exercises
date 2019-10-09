using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> GetCart(int userId);
        Task<Cart> AddProductsToCart(int userId, List<Product> products);
        Task<Cart> RemoveProductFromCart(int userId, int productId);

    }
}
