using System.Collections.Generic;
using Project0.StoreApplication.Domain;

namespace Project0.StoreApplication.Domain.Models
{
  /// <summary>
  /// 
  /// </summary>
  /// 
  public class Customer
  {
    public byte CustomerID {get;set;}
    public string Name { get; set; }
    public List<Order> Orders { get; } = new List<Order>();
    //public List<Post> Posts { get; } = new List<Post>();

   

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