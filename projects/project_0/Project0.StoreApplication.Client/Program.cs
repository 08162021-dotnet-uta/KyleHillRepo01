using System;
using System.Collections.Generic;
using Project0.StoreApplication.Domain.Models;
using Project0.StoreApplication.Storage.Repositories;

namespace Project0.StoreApplication.Client
{
  class Program
  {
    static void Main(string[] args)
    {
      var p = new Program();

      p.PrintAllStoreLocations();

      System.Console.WriteLine(p.SelectAStore());
    }

    void PrintAllStoreLocations()
    {
      var storeRepository = new StoreRepository();
      int i = 1;

      foreach (var store in storeRepository.Stores)
      {
        System.Console.WriteLine(i + " - " + store);
        i += 1;
      }
    }

    Store SelectAStore()
    {
      var sr = new StoreRepository().Stores;

      Console.WriteLine("Select a Store: ");

      var option = int.Parse(Console.ReadLine());
      var store = sr[option - 1];

      return store;
    }
  }
}
