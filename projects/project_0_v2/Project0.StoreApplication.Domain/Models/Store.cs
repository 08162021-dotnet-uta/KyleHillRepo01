using System.Collections.Generic;

namespace Project0.StoreApplication.Domain.Models
{
  /// <summary>
  /// 
  /// </summary>
  public class Store

  {
    public byte StoreID {get;set;}
    public string Name { get; set; }
    public List<Order> Orders { get; } = new List<Order>();

    public Store()
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
  

  }
}