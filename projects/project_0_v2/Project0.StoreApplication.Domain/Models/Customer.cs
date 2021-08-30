using System.Collections.Generic;
using Project0.StoreApplication.Domain;

namespace Project0.StoreApplication.Domain.Models
{
  /// <summary>
  /// 
  /// </summary>
  public class Customer
  {
    public byte CustomerID {get;set;}
    public string Name { get; set; }
    public List<Order> Orders { get; set; }

    public Customer()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      return $"{Name}";
    }

    //public void ViewPastOrders(AbstractSingleton orderSingleton)
    //{
    //    foreach (Order order in orderSingleton.Orders)
    //        if (order.Customer.Name.Equals(customer.Name))
    //            customer.Orders.Add(order);
    //    foreach (Order order in customer.Orders)
    //        Console.WriteLine(order);
    //    if (customer.Orders.Count == 0) Console.WriteLine("You have not placed any orders!");
    //    customer.Orders.Clear();
    //    PlaceOrderOrViewPastOrders(customer);
    //}

  }
}