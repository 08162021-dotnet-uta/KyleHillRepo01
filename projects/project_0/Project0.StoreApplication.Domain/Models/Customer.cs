namespace Project0.StoreApplication.Domain.Models
{
  public class Customer
  {
    public string Name { get; set; }

    public override string ToString()
    {
      return Name;
    }
  }
}