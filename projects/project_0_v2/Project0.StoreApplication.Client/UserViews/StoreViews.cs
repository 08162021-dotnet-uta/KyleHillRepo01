using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project0.StoreApplication.Domain.Models;
using Project0.StoreApplication.Client.Singletons;
using Serilog;

namespace Project0.StoreApplication.Client.UserViews
{
    static class StoreViews
    {
        private static readonly CustomerSingleton _customerSingleton = CustomerSingleton.Instance;
        private static readonly StoreSingleton _storeSingleton = StoreSingleton.Instance;
        private static readonly ProductSingleton _productSingleton = ProductSingleton.Instance;
        private static readonly OrderSingleton _orderSingleton = OrderSingleton.Instance;

        internal static void ViewOrdersByLocationOrTotal() 
        {
            Console.WriteLine("Would you like to view sales for the entire company(1) or sales by location(2)?");
            int input;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out input))
                {
                    if (input == 1) ViewPastOrder_EntireCompany();
                    else if (input == 2) ViewPastOrders_Location(_storeSingleton.Stores[Program.Capture<Store>(_storeSingleton.Stores)], _orderSingleton);
                    else if (input == 3) Environment.Exit(0);
                    //handles wrong integer input
                    else Console.WriteLine("Invalid input. Try Again.");
                }
                //handles not integer input
                else Console.WriteLine("Invalid input. Try Again");
            }
        }

        internal static void ViewPastOrder_EntireCompany() 
        {
            foreach (Order order in _orderSingleton.Orders) Console.WriteLine(order);
            ViewOrdersByLocationOrTotal();
                
        }

        internal static void ViewPastOrders_Location(Store store, OrderSingleton orderSingleton)
        {
            Log.Information("Store viewing past orders");
            foreach (Order order in orderSingleton.Orders)
                if (order.Store.Name.Equals(store.Name))
                    store.Orders.Add(order);
            foreach (Order order in store.Orders)
                Console.WriteLine(order);
            if (store.Orders.Count == 0) Console.WriteLine("No orders have been placed here!");
            store.Orders.Clear();
            ViewOrdersByLocationOrTotal();
        }

    }
}
