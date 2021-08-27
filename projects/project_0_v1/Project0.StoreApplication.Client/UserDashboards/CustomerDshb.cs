using Project0.StoreApplication.Domain.Models;
using System;
using System.Collections.Generic;
using Project0.StoreApplication.Storage.Repositories;

namespace Project0.StoreApplication.Client.UserDashboards
{
  class CustomerDshb
  {
    private Customer Customer;
    private List<MenuItem> Menu;
    private List<MenuItem> Cart;
    private Location Location;

    internal CustomerDshb(string name)
    {
      MenuItemRepository menuItemRepository = new MenuItemRepository();
      Menu = menuItemRepository.Menu;

      Customer customer = new Customer();
      customer.Name = name;
      Customer = customer;
      PlaceOrderOrViewPastOrders();
    }

    private void PlaceOrderOrViewPastOrders()
    {
      Console.WriteLine("Do you want to place an order(1) or view past orders(2)? Press (3) to close the program.");
      int input;
      while (true)
      {
        if (int.TryParse(Console.ReadLine(), out input))
        {
          if (input == 1) ViewMenu_AddToCart_PlaceOrder();
          else if (input == 2) ViewPastOrders();
          else if (input == 3) Environment.Exit(0);
          //handles wrong integer input
          else Console.WriteLine("Invalid input. Try Again.");
        }
        //handles not integer input
        else Console.WriteLine("Invalid input. Try Again");
      }
    }

    private void DisplayLocations()
    {

    }

    private void ChooseLocation()
    {
      Console.WriteLine("Choose a location:");
      DisplayLocations();
      int input;
      while (true)
      {
        if (int.TryParse(Console.ReadLine(), out input))
        {
          if (input == 1) ViewMenu_AddToCart_PlaceOrder();
          else if (input == 2) ViewPastOrders();
          else if (input == 3) Environment.Exit(0);
          //handles wrong integer input
          else Console.WriteLine("Invalid input. Try Again.");
        }
        //handles not integer input
        else Console.WriteLine("Invalid input. Try Again");
      }
    }

    private void ViewMenu_AddToCart_PlaceOrder()
    {
      //ChooseLocation();
      Cart = new List<MenuItem>();
      int input;
      while (true)
      {
        DisplayMenuAndCart();
        if (int.TryParse(Console.ReadLine(), out input))
        {
          if (input == 0) { }//PlaceOrder();
          else if (input > 0) Cart.Add(Menu[input - 1]);
          else if (input < 0) Cart.RemoveAt(-input - 1);
          //handles wrong integer input
          else Console.WriteLine("Invalid input. Try Again.");
        }
        //handles not integer input
        else Console.WriteLine("Invalid input. Try Again");
      }
    }

    private void DisplayMenuAndCart()
    {
      int index = 1;
      Console.WriteLine("MENU");
      foreach (MenuItem item in Menu)
      {
        Console.WriteLine($"{index,-3}|{item.Name,-20}|{item.Price,6}");
        index++;
      }
      Console.WriteLine("CART");
      index = -1;
      foreach (MenuItem item in Cart)
      {
        Console.WriteLine($"{index,-3}|{item.Name,-20}|{item.Price,6}");
        index--;
      }
      Console.WriteLine("Type 0 to submit your order.");
    }

    private void ViewPastOrders() { }

  }

}