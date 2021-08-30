using System.Collections.Generic;
using Project0.StoreApplication.Domain.Models;
using Project0.StoreApplication.Storage.Adapters;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Project0.StoreApplication.Storage.Repositories
{
  /// <summary>
  /// 
  /// </summary>
  public class ProductRepository 
  {
    //private const string _path = @"data/Products.xml";
    //private static readonly FileAdapter _fileAdapter = new FileAdapter();
    private readonly DataAdapter _dataAdapter = new DataAdapter();

    public ProductRepository()
    {
      //if (_fileAdapter.ReadFromFile<Product>(_path) == null)
      //{
      //  _fileAdapter.WriteToFile<Product>(_path, new List<Product>());
      //}
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public void Insert(List<Product> entries)
    {
      //_fileAdapter.WriteToFile<Product>(_path, entries);

      //return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public List<Product> Select()
    {
            return _dataAdapter.Products.FromSqlRaw("select * from Store.Product").ToList();
    }

  }
}