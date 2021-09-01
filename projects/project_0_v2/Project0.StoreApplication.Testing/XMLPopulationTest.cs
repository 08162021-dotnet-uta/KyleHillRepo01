using Xunit;
using Project0.StoreApplication.Storage.Repositories;
using static Project0.StoreApplication.Client.Program;
using Project0.StoreApplication.Storage.Adapters;
using Project0.StoreApplication.Domain.Models;
using Project0.StoreApplication.Client.Singletons;
using System.Collections.Generic;
using System;
using System.IO;

namespace Project0.StoreApplication.Testing
{
  //this is bad. don't try to emulate
  public class XMLPopulationTest
  {
        FileAdapter fileAdapter = new FileAdapter();
        [Fact]
        public void Test()
        {
            CustomerSingleton _customerSingleton = CustomerSingleton.Instance;
            List<Customer> customers = AddCustomers(_customerSingleton);
            Assert.True(fileAdapter.ReadFromFile<Customer>("data/Customers.xml").Count == 3);
            File.Delete("data/Customers.xml");        
        }
    }
}