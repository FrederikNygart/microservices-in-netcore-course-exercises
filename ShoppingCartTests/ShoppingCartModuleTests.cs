using EventConsumer;
using Marten;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy;
using Nancy.Testing;
using ShoppingCart;
using ShoppingCart.Models;
using ShoppingCart.Modules;
using ShoppingCart.Repositories;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingCartTests
{
    [TestClass]
    public class ShoppingCartModuleTests
    {
        Browser sut; // system under test

        [TestInitialize]
        public void TestInitialize()
        {
            sut = new Browser(with =>
            {
                with.Module<ShoppingCart.Modules.Cart>();
                with.Dependency<ICartRepository>(new FakeCartRepository());
                with.Dependency<IEventStore>(new FakeEventStore());
            },defaults => defaults.Accept("application/json"));
        }

        [TestMethod]
        public async Task AddProductsToCart()
        {
            var products = new List<Product>()
            {
                new Product {Id = 1, Name = "X-box"},
                new Product {Id = 2, Name = "Playstation"},
            };

            var response = await sut.Post(
                $"/shoppingcart/123", with => with.JsonBody(products));

            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);

            // TODO: Assert content of body
        }
    }
}
