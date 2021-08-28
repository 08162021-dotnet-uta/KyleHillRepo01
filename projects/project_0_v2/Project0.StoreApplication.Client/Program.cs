// procedural programming - DONE
// functional programming lite - DONE
// object-oriented programming - DONE
// unit testing - DONE
// generics - DONE
// design patterns: singleton - DONE
// structure: SOLID - DONE
// serialization - DONE
// monitoring - DONE
// debugging - DONE

using System;
using System.Collections.Generic;
using Project0.StoreApplication.Client.Singletons;
using Project0.StoreApplication.Domain.Models;
using Serilog;
using System.IO;
using Project0.StoreApplication.Storage.Adapters;

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
    private const string _logFilePath = @"data/logs.txt";


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

      
      if (_customerSingleton.Customers.Count == 0) AddCustomers(_customerSingleton);
      if (_storeSingleton.Stores.Count == 0) AddStores(_storeSingleton);
      if (_productSingleton.Products.Count == 0) AddProducts(_productSingleton);
      CustomerOrLocation();
    }

    public static List<Customer> AddCustomers(CustomerSingleton customerSingleton)
    {
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

    public static List<Product> AddProducts(ProductSingleton productSingleton)
    {
        Product cus1 = new Product();
        cus1.Name = "Cheese pizza"; cus1.Price = 10.99;
        Product cus2 = new Product();
        cus2.Name = "Calzone"; cus2.Price = 4.99;
        Product cus3 = new Product();
        cus3.Name = "Garlic bread"; cus3.Price = 8.99;
        List<Product> customers = new List<Product>() { cus1, cus2, cus3 };
        productSingleton.Add(customers);
        return customers;
    }

    public static List<Store> AddStores(StoreSingleton storeSingleton)
    {
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

    private static void CustomerOrLocation()
    {
      Console.WriteLine("Are you a customer(1) or location(2)? Press (3) to close the program.");
      int input;
      while (true)
      {
        if (int.TryParse(Console.ReadLine(), out input))
        {
          //need to do validation on this input
          if (input == 1) PlaceOrderOrViewPastOrders(_customerSingleton.Customers[Capture<Customer>(_customerSingleton.Customers)]);
          else if (input == 2) ViewPastOrders(_storeSingleton.Stores[Capture<Store>(_storeSingleton.Stores)]);
          else if (input == 3) Environment.Exit(0);
          //handles wrong integer input
          else Console.WriteLine("Invalid input. Try Again.");
        }
        //handles not integer input
        else Console.WriteLine("Invalid input. Try Again");
      }
    }

    private static void ViewPastOrders(Store store)
    {
        foreach (Order order in _orderSingleton.Orders)
            if (order.Store.Name.Equals(store.Name))
                store.Orders.Add(order);
        foreach (Order order in store.Orders)
            Console.WriteLine(order);
        if (store.Orders.Count == 0) Console.WriteLine("No orders have been placed here!");
        store.Orders.Clear();
    }

        private static void PlaceOrderOrViewPastOrders(Customer customer)
    {
      Console.WriteLine("Do you want to place an order(1) or view past orders(2)? Press (3) to close the program.");
      int input;
      while (true)
      {
        if (int.TryParse(Console.ReadLine(), out input))
        {
          if (input == 1) ViewMenu_AddToCart_PlaceOrder(customer, _storeSingleton.Stores[Capture<Store>(_storeSingleton.Stores)]);
          else if (input == 2) ViewPastOrders(customer);
          else if (input == 3) Environment.Exit(0);
          //handles wrong integer input
          else Console.WriteLine("Invalid input. Try Again.");
        }
        //handles not integer input
        else Console.WriteLine("Invalid input. Try Again");
      }
    }

    private static void ViewPastOrders(Customer customer) {

        foreach (Order order in _orderSingleton.Orders)
            if (order.Customer.Name.Equals(customer.Name))
                customer.Orders.Add(order);
        foreach (Order order in customer.Orders)
            Console.WriteLine(order);
        if (customer.Orders.Count == 0) Console.WriteLine("You have not placed any orders!");
        customer.Orders.Clear();
        PlaceOrderOrViewPastOrders(customer);
    }

    //displaying cart not part of MVP
    private static void ViewMenu_AddToCart_PlaceOrder(Customer customer, Store store)
    {
      List<Product> Cart = new List<Product>();
      List<Product> Menu = _productSingleton.Products;
      int input;
        double totalCost = 0;
      while (true)
      {
        DisplayMenuAndCart(Cart, Menu, totalCost);
        if (int.TryParse(Console.ReadLine(), out input))
        {
            if (input == 0)
                if (Cart.Count == 0) Console.WriteLine("You need at least one item in your cart to place an order.");
                else PlaceOrder(_orderSingleton, Cart, customer, store, totalCost);
            else if (input > 0)
                if (CanAddItemToCart(Cart, Menu, totalCost, input)) { Cart.Add(Menu[input - 1]); totalCost += Menu[input - 1].Price; }
                else Console.WriteLine("Cart max: 50 items, Cost max: $500");
            else if (input < 0) { Cart.RemoveAt(-input - 1); totalCost -= Menu[-input - 1].Price; }
            //handles wrong integer input
            else Console.WriteLine("Invalid input. Try Again.");
        }
        //handles not integer input
        else Console.WriteLine("Invalid input. Try Again");
      }
    }

    private static bool CanAddItemToCart(List<Product> cart, List<Product> menu, double totalCost, int input)
    {
            if (cart.Count + 1 <= 50 && totalCost + menu[input - 1].Price <= 500) return true;
            else return false;
    }

    public static void PlaceOrder(OrderSingleton orderSingleton,List<Product> Cart, Customer customer, Store store, double totalCost)
    {
        Order order = new Order();
        order.Products = Cart;
        order.Customer = customer;
        order.Store = store;
        order.TotalCost = totalCost;
        order.Date = DateTime.Now;       
        orderSingleton.Add(order);
        Console.WriteLine("Your order has been placed!");
        PlaceOrderOrViewPastOrders(customer);
    }

    private static void DisplayMenuAndCart(List<Product> Cart, List<Product> Menu, double totalCost)
    {
      int index = 1;
      Console.WriteLine("MENU");
      foreach (Product item in Menu)
      {
        Console.WriteLine($"{index,-3}|{item.Name,-20}|{item.Price,6}");
        index++;
      }
      Console.WriteLine("CART");
      index = -1;
      foreach (Product item in Cart)
      {
        Console.WriteLine($"{index,-3}|{item.Name,-20}|{item.Price,6}");
        index--;
      }
      Console.WriteLine($"Total Cost = {totalCost}");
      Console.WriteLine("Type 0 to submit your order.");
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
    private static int Capture<T>(List<T> data) where T : class
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
