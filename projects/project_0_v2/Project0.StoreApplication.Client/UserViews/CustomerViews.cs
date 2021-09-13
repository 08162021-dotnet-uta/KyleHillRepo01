using System;
using System.Collections.Generic;
using System.Linq;
using Project0.StoreApplication.Domain.Models;
using Project0.StoreApplication.Client.Singletons;
using Project0.StoreApplication.Client;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace Project0.StoreApplication.Client.UserViews
{
    
    
    public class CustomerViews
    {


        private static readonly CustomerSingleton _customerSingleton = CustomerSingleton.Instance;
        private static readonly StoreSingleton _storeSingleton = StoreSingleton.Instance;
        private static readonly ProductSingleton _productSingleton = ProductSingleton.Instance;
        private static readonly OrderSingleton _orderSingleton = OrderSingleton.Instance;


        internal static void PlaceOrderOrViewPastOrders(Customer customer, StoreSingleton _storeSingleton)
        {

            Log.Information("Customer choosing between place order and view past orders");
            Console.WriteLine("Do you want to place an order(1) or view past orders(2)? Press (3) to close the program.");
            int input;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out input))
                {
                    if (input == 1) ViewMenu_AddToCart_PlaceOrder(customer, _storeSingleton.Stores[Program.Capture<Store>(_storeSingleton.Stores)]);
                    else if (input == 2) ViewPastOrders(customer);
                    else if (input == 3) Environment.Exit(0);
                    //handles wrong integer input
                    else Console.WriteLine("Invalid input. Try Again.");
                }
                //handles not integer input
                else Console.WriteLine("Invalid input. Try Again");
            }
        }

        private static void ViewPastOrders(Customer customer)
        {

            Log.Information($"{customer.Name} viewed past orders");
            //Log.Information("Customer viewing past orders");
            foreach (Order order in _orderSingleton.Orders)
                if (order.Customer.Name.Equals(customer.Name))
                    customer.Orders.Add(order);
            foreach (Order order in customer.Orders)
                Console.WriteLine(order);
            if (customer.Orders.Count == 0) Console.WriteLine("You have not placed any orders!");
            customer.Orders.Clear();
            PlaceOrderOrViewPastOrders(customer,_storeSingleton);
        }

        private static void ViewMenu_AddToCart_PlaceOrder(Customer customer, Store store)
        {
            Log.Information("Customer manipulating their cart");
            List<Product> Cart = new List<Product>();
            List<Product> Menu = _productSingleton.Products;
            string input;
            int IntInput;
            char CharInput;
            double totalCost = 0;
            while (true)
            {
                DisplayMenuAndCart(Cart, Menu, totalCost);
                input = Console.ReadLine();
                if (int.TryParse(input, out IntInput))
                {
                    if (IntInput == 0)
                        if (Cart.Count == 0) { Log.Information("Tried placing order without items"); Console.WriteLine("You need at least one item in your cart to place an order."); }
                        else { PlaceOrder(_orderSingleton, Cart, customer, store, totalCost); PlaceOrderOrViewPastOrders(customer, _storeSingleton); }
                    else if (IntInput > 0)
                        if (CanAddItemToCart(Cart, Menu, totalCost, IntInput)) AddItemToCart(ref Cart, ref totalCost, Menu, IntInput);
                        else Console.WriteLine("Cart max: 50 items, Cost max: $500");
                    else if (IntInput < 0) RemoveItemFromCart(ref Cart, ref totalCost, Menu, IntInput);
                    //handles wrong integer input
                    else Console.WriteLine("Invalid input. Try Again.");
                }
                //handles not integer input
                else if (char.TryParse(input, out CharInput))
                    if (CharInput == 'q') { Console.WriteLine("Your order was cancelled!"); PlaceOrderOrViewPastOrders(customer, _storeSingleton); }
                    else Console.WriteLine("Invalid input. Try Again");
                else Console.WriteLine("Invalid input. Try Again");
            }
        }

        public static void AddItemToCart(ref List<Product> cart, ref double totalCost, List<Product> menu, int input)
        {
            cart.Add(menu[input - 1]); totalCost += (double)menu[input - 1].Price;
        }

        public static void RemoveItemFromCart(ref List<Product> cart, ref double totalCost, List<Product> menu, int input)
        {
            double priceToRemove = (double)cart[-input - 1].Price; 
            cart.RemoveAt(-input - 1); 
            totalCost -= priceToRemove;
        }

        public static bool CanAddItemToCart(List<Product> cart, List<Product> menu, double totalCost, int input)
        {
            if (cart.Count + 1 <= 50 && totalCost + (double)menu[input - 1].Price <= 500) return true;
            else return false;
        }

        public static Order PlaceOrder(OrderSingleton orderSingleton, List<Product> Cart, Customer customer, Store store, double totalCost)
        {
            Log.Information("Customer placed order");
            Order order = new Order();
            //order.Products = Cart;
            order.Customer = customer;
            order.Store = store;
            order.TotalCost = totalCost;
            order.Date = DateTime.Now;
            orderSingleton.Add(order);
            Console.WriteLine("Your order has been placed!");
            return order;
        }

        private static void DisplayMenuAndCart(List<Product> Cart, List<Product> Menu, double totalCost)
        {
            int index = 1;
            Console.WriteLine("MENU");
            foreach (Product item in Menu)
            {
                Console.WriteLine($"{index,-3}|{item.Name,-20}|{Decimal.Round(item.Price, 2),6}");
                index++;
            }
            Console.WriteLine("CART");
            index = -1;
            foreach (Product item in Cart)
            {
                Console.WriteLine($"{index,-3}|{item.Name,-20}|{Decimal.Round(item.Price, 2),6}");
                index--;
            }
            Console.WriteLine($"Total Cost = {totalCost}");
            Console.WriteLine("Type 0 to submit your order. Type 'q' to cancel your order.");
        }

    }
}
