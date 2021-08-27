using Xunit;
using Project0.StoreApplication.Storage.Repositories;
using static Project0.StoreApplication.Client.Program;
using Project0.StoreApplication.Storage.Adapters;
using Project0.StoreApplication.Domain.Models;
using Project0.StoreApplication.Client.Singletons;
using System.Collections.Generic;
using System;

namespace Project0.StoreApplication.Testing
{
  public class StoreRepositoryTests
  {
    // FileAdapter fileAdapter = new FileAdapter();
    // [Fact]
    // public void Test()
    // {
      
    //   CustomerSingleton _customerSingleton = CustomerSingleton.Instance;
      
    //  // ProductSingleton _productSingleton = ProductSingleton.Instance;
      
    //   //StoreSingleton _storeSingleton = StoreSingleton.Instance;
      
    //   List<Customer> customers = AddCustomers(_customerSingleton);
    //   //List<Product> products = AddProducts(_productSingleton);
    //   //List<Store> stores = AddStores(_storeSingleton);
    //   // foreach (Customer item in customers)
    //   // Console.WriteLine(item.ToString());
    //   // List <Customer> result = fileAdapter.ReadFromFile<Customer>("/home/kylehill/revature-code/my_code/data/Customers.xml");
    //   // foreach (Customer item in result)
    //   // Console.WriteLine(item.ToString());
    //   Customer cus1 = new Customer();
    //     cus1.Name = "bob";
    //     Customer cus2 = new Customer();
    //     cus2.Name = "sally";
    //     Customer cus3 = new Customer();
    //     cus3.Name = "george";
    //     List<Customer> whatever = new List<Customer>() { cus1, cus2, cus3 };
    //   Assert.Equal(whatever,fileAdapter.ReadFromFile<Customer>("/home/kylehill/revature-code/my_code/data/Customers.xml"));
    //   //Assert.Equal(products,fileAdapter.ReadFromFile<Product>("/home/kylehill/revature-code/my_code/data/Products.xml"));
    //   //Assert.Equal(stores,fileAdapter.ReadFromFile<Store>("/home/kylehill/revature-code/my_code/data/Stores.xml"));
    //}
  }
}