using System.Collections.Generic;

namespace Project0.StoreApplication.Domain.Models
{
  /// <summary>
  /// 
  /// </summary>
  public class Order
  {
    public byte OrderID {get;set;}
    public List<Product> Products { get; set; }
    public Store Store { get; set; }
    public Customer Customer { get; set; }
  }
}