using System.Collections.Generic;

namespace Project0.StoreApplication.Domain.Interfaces
{
  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public interface IRepository<T> where T : class
  {
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    bool Delete();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    bool Insert(List<T> entry);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    List<T> Select();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    T Update();
  }
}