using System.Collections.Generic;
using Project0.StoreApplication.Domain.Models;
using Project0.StoreApplication.Storage.Repositories;
using Serilog;

namespace Project0.StoreApplication.Client.Singletons
{
  /// <summary>
  /// 
  /// </summary>
  public class ProductSingleton
  {
    private static ProductSingleton _productSingleton;
    private static readonly ProductRepository _productRepository = new ProductRepository();

    public List<Product> Products { get; private set; }
    public static ProductSingleton Instance
    {
      get
      {
        if (_productSingleton == null)
        {
          _productSingleton = new ProductSingleton();
        }

        return _productSingleton;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    private ProductSingleton()
    {
      Log.Information("Instantiate product singleton");
      Products = _productRepository.Select();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="products"></param>
    public void Add(List<Product> products)
    {
      _productRepository.Insert(products);
      Products = _productRepository.Select();
    }
  }
}