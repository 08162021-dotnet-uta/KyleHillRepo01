// procedural programming - DONE
// functional programming lite - DONE
// object-oriented programming - DONE
// unit testing - DONE
// design patterns: singleton, factory - NEXT
// structure: SOLID - NEXT
// serialization - DONE
// monitoring - DONE
// debugging - DONE

using System;
using Project0.StoreApplication.Storage.Repositories;
using Serilog;


namespace Project0.StoreApplication.Client
{
  class Program
  {
    static void Main(string[] args)
    {
      Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

      var program = new Program();

      program.CaptureOutput();

    }

    private void OutputStores()
    {
      // verbose
      // debug
      // info
      // warn
      // error
      // fatal

      Log.Information("mehtod outpoutstores");

      var storeRepository = new StoreRepository();

      foreach (var store in storeRepository.Stores)
      {
        Console.WriteLine(store);
      }
    }

    private int CaptureInput()
    {
      Log.Information("in method captureinput");

      OutputStores();

      Console.WriteLine("pick a store:");

      int selected = int.Parse(Console.ReadLine());

      return selected;
    }

    private void CaptureOutput()
    {
      var storeRepository = new StoreRepository();

      Console.WriteLine("you have selected: " + " " + storeRepository.Stores[CaptureInput()]);
    }
  }
}
