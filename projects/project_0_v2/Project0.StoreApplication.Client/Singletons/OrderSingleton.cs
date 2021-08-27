using System.Collections.Generic;
using Project0.StoreApplication.Domain.Models;
using Project0.StoreApplication.Storage.Repositories;

namespace Project0.StoreApplication.Client.Singletons
{
  /// <summary>
  /// 
  /// </summary>
  public class OrderSingleton
  {
    private static OrderSingleton _orderSingleton;
    private static readonly OrderRepository _orderRepository = new OrderRepository();

    public List<Order> Orders { get; private set; }
    public static OrderSingleton Instance
    {
      get
      {
        if (_orderSingleton == null)
        {
          _orderSingleton = new OrderSingleton();
        }

        return _orderSingleton;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    private OrderSingleton()
    {
      Orders = _orderRepository.Select();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="orders"></param>
    public void Add(Order order)
    {
      Orders.Add(order);
      _orderRepository.Insert(Orders);
      Orders = _orderRepository.Select();
    }
  }
}