using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Project0.StoreApplication.Domain.Models;
using Project0.StoreApplication.Client.UserViews;


namespace Project0.StoreApplication.Testing
{
    public class CustomerViewsTesting
    {

        [Theory]
        [InlineData(49,70)]
        public void CanAddItemToCartTest_True(int currentCartCount, double currentTotalCost)
        {
            List<Product> cart = new List<Product>();
            Product product = new Product();
            product.Price = 5;
            List<Product> menu = new List<Product>() { product };
            for (int i = 1; i <= currentCartCount; i++) cart.Add(product);
            Assert.True(CustomerViews.CanAddItemToCart(cart, menu, currentTotalCost, 1));
        
        }

        [Theory]
        [InlineData(50,70)] //too many items
        [InlineData(45,499)] //balance too large
        public void CanAddItemToCartTest_False(int currentCartCount, double currentTotalCost)
        {
            List<Product> cart = new List<Product>();
            Product product = new Product();
            product.Price = 5;
            List<Product> menu = new List<Product>() { product };
            for (int i = 1; i <= currentCartCount; i++) cart.Add(product);
            Assert.False(CustomerViews.CanAddItemToCart(cart, menu, currentTotalCost, 1));
        }

        [Fact]
        public void AddItemToCartTest()
        {
            List<Product> cart = new List<Product>();
            Product product = new Product();
            product.Price = 5;
            List<Product> menu = new List<Product>() { product };
            double totalCost = 0;
            CustomerViews.AddItemToCart(ref cart, ref totalCost, menu, 1);
            Assert.True(cart.Count == 1 && cart[0].Price == product.Price && totalCost == 5);
        }
    }
}
