using System.Collections.Generic;
using Project0.StoreApplication.Domain.Models;
using Project0.StoreApplication.Storage.Repositories;

namespace Project0.StoreApplication.Client.Singletons
{
  /// <summary>
  /// 
  /// </summary>
  public class StoreSingleton
  {
    private static StoreSingleton _storeSingleton;
    private static readonly StoreRepository _storeRepository = new StoreRepository();

    public List<Store> Stores { get; private set; }
    public static StoreSingleton Instance
    {
      get
      {
        if (_storeSingleton == null)
        {
          _storeSingleton = new StoreSingleton();
        }

        return _storeSingleton;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    private StoreSingleton()
    {
      Stores = _storeRepository.Select();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Store"></param>
    public void Add(List<Store> stores)
    {
      _storeRepository.Insert(stores);
      Stores = _storeRepository.Select();
    }
  }
}