using System;

namespace Project0.StoreApplication.Domain.Models
{
  /// <summary>
  /// 
  /// </summary>
  public class Product
  {
    public byte ProductID {get;set;}
    public string Name { get; set; }
    public decimal Price { get; set; }

    public override string ToString()
    {
        return $"{Name} | {Decimal.Round(Price,2)}";
    }


    }
}