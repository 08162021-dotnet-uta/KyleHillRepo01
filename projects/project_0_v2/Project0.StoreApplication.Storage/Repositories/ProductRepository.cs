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
    private readonly DataAdapter _dataAdapter = new DataAdapter();

    public ProductRepository()
    {
      
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public void Insert(List<Product> entries)
    {
      _dataAdapter.Database.ExecuteSqlRaw("insert into [Store].Product values ('chicken poppers',3.99,1);");
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