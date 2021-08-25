using System;
using Project0.StoreApplication.Storage.Repositories;
using Serilog;
using Project0.StoreApplication.Domain.Models;
using System.Collections.Generic;
using Project0.StoreApplication.Storage.Adapters;
using Project0.StoreApplication.Client.UserDashboards;

namespace Project0.StoreApplication.Client
{
  class Program
  {
    static void Main(string[] args)
    {
      //Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
      var program = new Program();
      //program.CustomerOrLocation();
      //CustomerDshb testing = new CustomerDshb("yo momma");
      // List<Location> input = new List<Location>() { new Location(), new Location(), new Location() };
      // input[0].Name = "Amherst"; input[1].Name = "Elyria"; input[2].Name = "Avon";
      // //input[0].Price = 5.99; input[1].Price = 10.99; input[2].Price = 11.99;
      // FileAdapter adapter = new FileAdapter();
      // adapter.WriteToFile<Location>("/home/kylehill/revature-code/my_code/data/Locations.xml", input);
      //List<Customer> output = adapter.ReadFromFile<Customer>("/home/kylehill/revature-code/my_code/data/Customers.xml");
      //program.CaptureOutput();
      int balls = 5;
    }

    //Use delegates to reduce the repetition of 3-option code? 
    //TESTED that this logic works correctly
    //TESTED that the while loop behaves as desired
    private void CustomerOrLocation()
    {
      Console.WriteLine("Are you a customer(1) or location(2)? Press (3) to close the program.");
      int input;
      while (true)
      {
        if (int.TryParse(Console.ReadLine(), out input))
        {
          if (input == 1) NeworExistingCustomer();
          else if (input == 2) Console.WriteLine("input == 2");
          else if (input == 3) Environment.Exit(0);
          //handles wrong integer input
          else Console.WriteLine("Invalid input. Try Again.");
        }
        //handles not integer input
        else Console.WriteLine("Invalid input. Try Again");
      }
    }

    private void NeworExistingCustomer()
    {
      Console.WriteLine("Are you a new(1) or existing(2) customer?");
      int input;
      while (true)
      {
        if (int.TryParse(Console.ReadLine(), out input))
        {
          if (input == 1) NewCustomer();
          else if (input == 2) ExistingCustomer();
          //handles wrong integer input
          else Console.WriteLine("Invalid input.");
        }
        //handles not integer input
        else Console.WriteLine("Invalid input.");
      }
    }

    //TESTED
    private void ExistingCustomer()
    {
      Console.WriteLine("Please provide your name:");
      String name;
      while (true)
      {
        name = Console.ReadLine();
        if (!nameIsUnique(name)) new CustomerDshb(name);
        else
        {
          Console.WriteLine("We can't find you in the system. Try Again.");
        }
      }
    }

    //
    private void NewCustomer()
    {
      Console.WriteLine("Please provide your name. This will be used for signing into your account:");
      String name;
      //check if name is available
      while (true)
      {
        name = Console.ReadLine();
        //handle case when Customers.xml is empty
        if (nameIsUnique(name)) new CustomerDshb(name);
        else
        {
          Console.WriteLine("That name is already taken. Try Again.");
        }
      }
    }

    //TESTED
    private bool nameIsUnique(string name)
    {
      CustomerRepository customerRepository = new CustomerRepository();
      for (int i = 0; i < customerRepository.Customers.Count; i++)
        if (customerRepository.Customers[i].Name.Equals(name)) return false;
      return true;
    }

    //TESTED
    private void AddNewCustomerToRepository(string name)
    {
      CustomerRepository customerRepository = new CustomerRepository();
      customerRepository.AddNewCustomer(name);
    }

    static bool Input(out int i1, out int i2)
    {
      // input stuff
      // int input1, input2;

      if (int.TryParse(Console.ReadLine(), out i1) & int.TryParse(Console.ReadLine(), out i2))
      {
        // i1 = input1;
        // i2 = input2;

        return true;
      }
      else
      {
        // i1 = i2 = 0;

        return false;
      }
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
