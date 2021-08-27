using Project0.StoreApplication.Domain.Models;
using Xunit;
using static Project0.StoreApplication.Client.Program;
using Project0.StoreApplication.Client.Singletons;
using System.Collections.Generic;
using Project0.StoreApplication.Storage.Adapters;

namespace Project0.StoreApplication.Testing
{
  public class OrderTesting{
    [Fact]

    public void Help(){
    
      //FileAdapter fileAdapter = new FileAdapter();
      OrderSingleton orderSingleton = OrderSingleton.Instance;
      Product cheese = new Product();
      cheese.Name = "cheese"; cheese.Price = 2.99;
      Product corn = new Product();
      corn.Name = "corn"; corn.Price = 4.99;
      List<Product> cart = new List<Product>(){cheese,corn};
      Customer bill = new Customer();
      bill.Name = "bill";
      Store amherst = new Store();
      amherst.Name = "amherst";
      Order order = PlaceOrder(orderSingleton,cart,bill,amherst);
      Order fromDB = new Order();
      foreach(Order item in orderSingleton.Orders)
        if (item.Customer.Equals("bill")) fromDB = item;
      Assert.Equal(order,fromDB);
    }
  }
}