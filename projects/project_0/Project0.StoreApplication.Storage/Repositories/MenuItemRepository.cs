using Project0.StoreApplication.Domain.Models;
using System.Collections.Generic;
using Project0.StoreApplication.Storage.Adapters;

namespace Project0.StoreApplication.Storage.Repositories
{
  public class MenuItemRepository
  {
    public List<MenuItem> Menu { get; }
    FileAdapter fileAdapter = new FileAdapter();
    public MenuItemRepository()
    {
      Menu = fileAdapter.ReadFromFile<MenuItem>("/home/kylehill/revature-code/my_code/data/MenuItems.xml");
    }
  }
}