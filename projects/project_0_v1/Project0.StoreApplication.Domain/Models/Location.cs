using System.Collections.Generic;

namespace Project0.StoreApplication.Domain.Models
{
  public class Location
  {
    public string Name { get; set; }
    public List<Order> OrderList { get; set; }
  }
}