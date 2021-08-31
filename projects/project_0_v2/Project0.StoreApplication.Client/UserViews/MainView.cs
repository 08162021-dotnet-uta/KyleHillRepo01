using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project0.StoreApplication.Client.UserViews;
using Project0.StoreApplication.Client;
using Project0.StoreApplication.Domain.Models;
using Project0.StoreApplication.Client.Singletons;
using Serilog;

namespace Project0.StoreApplication.Client.UserViews
{
    class MainView
    {
        internal static void CustomerOrLocation(CustomerSingleton customerSingleton, StoreSingleton storeSingleton, OrderSingleton orderSingleton)
        {
            Log.Information("User chooses whether they are customer or location");
            Console.WriteLine("Welcome to Kyle's Pizza Shop. Are you a customer(1) or employee(2)? Press (3) to close the program.");
            int input;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out input))
                {
                    if (input == 1) CustomerViews.PlaceOrderOrViewPastOrders(customerSingleton.Customers[Program.Capture<Customer>(customerSingleton.Customers)], storeSingleton);
                    else if (input == 2) StoreViews.ViewOrdersByLocationOrTotal();
                    else if (input == 3) Environment.Exit(0);
                    //handles wrong integer input
                    else Console.WriteLine("Invalid input. Try Again.");
                }
                //handles not integer input
                else Console.WriteLine("Invalid input. Try Again");
            }
        }
    }
}
