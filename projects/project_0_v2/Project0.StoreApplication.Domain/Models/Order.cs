using System.Collections.Generic;
using System;

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
    public double TotalCost { get; set; }
    public DateTime Date { get; set; }

    public override string ToString()
    {
        string firstPart =  $"-----------------\n{Store} | {Customer}";
        string products = "";
        foreach (Product product in Products)
            products = products + '\n' + product.ToString();
        return firstPart + '\n' + Date + products + $"\nTotal Cost: {TotalCost}";
    }
    }
}