using System;
using System.Collections.Generic;
using Project0.StoreApplication.Client.Singletons;
using Project0.StoreApplication.Domain.Models;
using Serilog;
using Project0.StoreApplication.Client.UserViews;

namespace Project0.StoreApplication.Client
{
  /// <summary>
  /// Defines the Program Class
  /// </summary>
  public class Program
  {
    private static readonly CustomerSingleton _customerSingleton = CustomerSingleton.Instance;
    private static readonly StoreSingleton _storeSingleton = StoreSingleton.Instance;
    private static readonly ProductSingleton _productSingleton = ProductSingleton.Instance;
    private static readonly OrderSingleton _orderSingleton = OrderSingleton.Instance;
    private const string _logFilePath = "data/logs.txt";


    /// <summary>
    /// Defines the Main Method
    /// </summary>
    /// <param name="args"></param>
    private static void Main(string[] args)
    { 
      Log.Logger = new LoggerConfiguration().WriteTo.File(_logFilePath).CreateLogger();
      Run();
      
       

        }

    /// <summary>
    /// 
    /// </summary>
    private static void Run()
    {
      Log.Information("method: Run()");


      if (_customerSingleton.Customers.Count == 0) { Log.Information("Loading customer list into XML file"); AddCustomers(_customerSingleton); }
      if (_storeSingleton.Stores.Count == 0) AddStores(_storeSingleton);
      MainView.CustomerOrLocation(_customerSingleton,_storeSingleton,_orderSingleton);
    }

    public static List<Customer> AddCustomers(CustomerSingleton customerSingleton)
    {
        Log.Information("Loading customers to file");
        Customer cus1 = new Customer();
        cus1.Name = "bob";
        Customer cus2 = new Customer();
        cus2.Name = "sally";
        Customer cus3 = new Customer();
        cus3.Name = "george";
        List<Customer> customers = new List<Customer>() { cus1, cus2, cus3 };
        customerSingleton.Add(customers);
        return customers;
    }

    public static List<Store> AddStores(StoreSingleton storeSingleton)
    {
        Log.Information("Loading stores to file");
        Store cus1 = new Store();
        cus1.Name = "Amherst";
        Store cus2 = new Store();
        cus2.Name = "Elyria";
        Store cus3 = new Store();
        cus3.Name = "Wellington";
        List<Store> customers = new List<Store>() { cus1, cus2, cus3 };
        storeSingleton.Add(customers);
        return customers;
    }

    

    /// <summary>
    /// 
    /// </summary>
    private static void Output<T>(List<T> data) where T : class
    {
      Log.Information($"method: Output<{typeof(T)}>()");

      var index = 0;

      foreach (var item in data)
      {
        Console.WriteLine($"[{++index}] - {item}");
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    internal static int Capture<T>(List<T> data) where T : class
    {
      Log.Information("method: Capture()");

      Output<T>(data);
      Console.Write("make selection: ");
      int input;
      while (true)
      {
        if (int.TryParse(Console.ReadLine(), out input))
        {
          if (1 > input || input > data.Count) Console.WriteLine("Invalid input. Try Again.");
          else return (input - 1);
        }
        //handles not integer input
        else Console.WriteLine("Invalid input. Try Again");
      }
    }
  }
}
