using System.Collections.Generic;

namespace Project0.StoreApplication.Domain.Models
{
  public class Order
  {
    public string StoreName { get; set; }
    public string CustomerName { get; set; }
    public List<MenuItem> MenuItemList { get; set; }
    public double TotalPrice { get; set; }
  }
}