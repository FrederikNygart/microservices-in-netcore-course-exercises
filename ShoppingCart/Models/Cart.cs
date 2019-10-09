using System.Collections.Generic;

namespace ShoppingCart.Models
{
    public class Cart
    {
        public int Id;
        public List<Product> Products { get; set; }
    }
}