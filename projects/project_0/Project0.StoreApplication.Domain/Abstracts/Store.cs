using System.Xml.Serialization;
using Project0.StoreApplication.Domain.Models;

namespace Project0.StoreApplication.Domain.Abstracts
{
  [XmlInclude(typeof(AthleticStore))]
  [XmlInclude(typeof(GroceryStore))]
  [XmlInclude(typeof(OnlineStore))]
  [XmlInclude(typeof(Customer))]
  public abstract class Store
  {
    public string Name { get; set; }

    public override string ToString()
    {
      return Name;
    }
  }
}
