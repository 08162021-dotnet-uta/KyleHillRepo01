using Project0.StoreApplication.Domain.Models;
using System.Collections.Generic;
using Project0.StoreApplication.Storage.Adapters;

namespace Project0.StoreApplication.Storage.Repositories
{
  public class LocationRepository
  {
    public List<Location> Locations { get; }
    FileAdapter fileAdapter = new FileAdapter();
    public LocationRepository()
    {
      Locations = fileAdapter.ReadFromFile<Location>("/home/kylehill/revature-code/my_code/data/Locations.xml");
    }
  }
}