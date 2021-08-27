using System.Collections.Generic;
using Project0.StoreApplication.Domain.Models;
using Project0.StoreApplication.Storage.Adapters;

namespace Project0.StoreApplication.Storage.Repositories
{
  public class CustomerRepository
  {
    public List<Customer> Customers { get; }
    FileAdapter fileAdapter = new FileAdapter();

    public CustomerRepository()
    {
      Customers = fileAdapter.ReadFromFile<Customer>("/home/kylehill/revature-code/my_code/data/Customers.xml");
    }

    public void AddNewCustomer(string name)
    {
      Customer newCustomer = new Customer();
      newCustomer.Name = name;
      Customers.Add(newCustomer);
      fileAdapter.WriteToFile<Customer>("/home/kylehill/revature-code/my_code/data/Customers.xml", Customers);
    }
  }
}
