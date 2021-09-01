using Xunit;
using Project0.StoreApplication.Client.Singletons;
using System.Collections.Generic;
using Project0.StoreApplication.Domain.Models;
using System.IO;

namespace Project0.StoreApplication.Testing
{
  public class SingletonTests
  {

    [Fact]
  //this is trash. don't view right now
    public void Testing()
    {
     
      CustomerSingleton _customerSingleton = CustomerSingleton.Instance;
      List<Customer> customers = _customerSingleton.Customers;
      Assert.Empty(customers);

    OrderSingleton _orderSingleton = OrderSingleton.Instance;
    List<Order> orders = _orderSingleton.Orders;
    Assert.Empty(orders);

            StoreSingleton _storeSingleton = StoreSingleton.Instance;
      List<Store> stores = _storeSingleton.Stores;
      Assert.Empty(stores);

            File.Delete("data/Customers.xml");
            File.Delete("data/Orders.xml");
            File.Delete("data/Stores.xml");
    }

  }


}