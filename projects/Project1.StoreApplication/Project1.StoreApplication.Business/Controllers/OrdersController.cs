using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.StoreApplication.Domain.Models;
using Project1.StoreApplication.Domain.ViewModels;
using Project1.StoreApplication.Domain.InputModels;

namespace Project1.StoreApplication.Business.Controllers
{
    [Route("html/api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly Kyles_Pizza_ShopContext _context;

        public OrdersController(Kyles_Pizza_ShopContext context)
        {
            _context = context;
        }

        //method is async because copy pasted the template
        // GET: api/Customers/userType_and_id
        [HttpGet("idType={idType}&id={id}")]
        public async Task<ActionResult<IEnumerable<OrderView>>> GetOrders(string idType, int id)
        {
            List<Order> orders = new List<Order>();
            List<OrderItem> orderItems = new List<OrderItem>();
            List<Customer> customers = new List<Customer>();
            List<Location> locations = new List<Location>();
            List<string> orderIdList = new List<string>();
            List<Product> products = new List<Product>();
            List<OrderView> orderViews = new List<OrderView>();
            List<OrderItemView> orderItemViews = new List<OrderItemView>();
            FormattableString orderQuery;
            if (idType.Equals("customer")) orderQuery = $"select * from Orders where CustomerID = {id} and OrderDate > {Order.cartOrderDate}  order by OrderDate";
            else orderQuery = $"select * from Orders where LocationID = {id} and OrderDate > {Order.cartOrderDate} order by OrderDate";

            //List<int> locationIdList = new List<int>();
            orders = _context.Orders.FromSqlRaw<Order>(orderQuery.ToString()).ToList();
            //foreach (Order order in orders)
            //{ 
            //    if (!locationIdList.Contains(order.LocationId))
            //        locationIdList.Add(order.LocationId);
            //    orderIdList.Add(order.Id.ToString());
            //}
            //string locationIdListString = string.Join<int>(",", locationIdList);
            //string orderIdListString = '\''+ string.Join<string>("','", orderIdList) + '\'';
            //orderItems = _context.OrderItems.FromSqlRaw<OrderItem>($"select * from OrderItems where OrderId in ({orderIdListString})").ToList();
            //customers = _context.Customers.FromSqlRaw<Customer>($"select * from Customers where Id = {id}").ToList();
            //locations = _context.Locations.FromSqlRaw<Location>($"select * from Locations where Id in ({locationIdListString})").ToList();
            //products = _context.Products.FromSqlRaw<Product>($"select * from Products").ToList();
            orderItems = _context.OrderItems.FromSqlRaw<OrderItem>($"select * from OrderItems").ToList();
            customers = _context.Customers.FromSqlRaw<Customer>($"select * from Customers").ToList();
            locations = _context.Locations.FromSqlRaw<Location>($"select * from Locations").ToList();
            products = _context.Products.FromSqlRaw<Product>($"select * from Products").ToList();

            foreach (Order order in orders)
            {
                OrderView orderView = new OrderView()
                {
                    CustomerId = order.CustomerId,
                    Id = order.Id,
                    LocationId = order.LocationId,
                    OrderDate = order.OrderDate,
                    TotalPrice = order.TotalPrice
                };
                orderViews.Add(orderView);
            }

            foreach (OrderItem orderItem in orderItems)
            {
                OrderItemView orderItemView = new OrderItemView();
                {
                    orderItemView.Id = orderItem.Id;
                    orderItemView.OrderId = orderItem.OrderId;
                    orderItemView.ProductId = orderItem.ProductId;
                }
                orderItemViews.Add(orderItemView);        
            }
            foreach (OrderItemView orderItemView in orderItemViews) 
                foreach (Product product in products)
                    if (product.Id == orderItemView.ProductId)
                        {orderItemView.Name1 = product.Name1; orderItemView.ProductPrice = product.ProductPrice;}
                

            foreach (OrderView orderView in orderViews) {
                foreach (OrderItemView orderItemView in orderItemViews)
                    if (orderView.Id == orderItemView.OrderId)
                        orderView.OrderItems.Add(orderItemView);
                foreach (Customer customer in customers)
                    if (orderView.CustomerId == customer.Id)
                        orderView.CustomerName = customer.FirstName + " " + customer.LastName;
                foreach (Location location in locations)
                    if (orderView.LocationId == location.Id)
                        orderView.LocationName = location.CityName; 
            }
            return orderViews;
            
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }



        [HttpPut("submitOrder")]
        public void submitOrder(OrderInput order)
        {
            _context.Database.ExecuteSqlRaw($"update Orders set OrderDate = getdate() where Id = '{order.OrderId}'");
            _context.SaveChanges();
        }


// PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public OrderView PutOrder(OrderInput order)
        {
            Order orderOfInterest= _context.Orders.FromSqlRaw<Order>($"select * from Orders where Id = '{order.OrderId}'").First();

            //handles simple add of new orderItem
            if (LocationInventory.itemIsAvailable(orderOfInterest.LocationId, order.ProductId) && order.Action.Equals("add"))
            {
                Product product = _context.Products.FromSqlRaw<Product>($"select * from Products where Id = {order.ProductId}").First();
                decimal totalPrice = orderOfInterest.TotalPrice + product.ProductPrice;
                _context.Database.ExecuteSqlRaw($"update Orders set TotalPrice = {totalPrice} where Id = '{order.OrderId}'");
                _context.Database.ExecuteSqlRaw($"insert into OrderItems (OrderId,ProductId) values ('{order.OrderId}',{product.Id})");
                _context.Database.ExecuteSqlRaw($"update LocationInventory set Stock = Stock - 1 where ProductId = {product.Id} and LocationId = {orderOfInterest.LocationId}");
                _context.SaveChanges();

                OrderView orderView = new OrderView()
                {
                    TotalPrice = totalPrice,
                    OrderItems = OrderView.GetOrderItemViews(order.OrderId),
                    actionSucceeded = true
                };

                return orderView;
            }
            else if (order.Action.Equals("remove") && itemIsInCart(order.OrderId,order.ProductId))
            {
                Product product = _context.Products.FromSqlRaw<Product>($"select * from Products where Id = {order.ProductId}").First();
                decimal totalPrice = orderOfInterest.TotalPrice - product.ProductPrice;
                _context.Database.ExecuteSqlRaw($"update Orders set TotalPrice = {totalPrice} where Id = '{order.OrderId}'");
                _context.Database.ExecuteSqlRaw($"delete top (1) from OrderItems where OrderId = '{order.OrderId}' and ProductId = {product.Id}");
                _context.Database.ExecuteSqlRaw($"update LocationInventory set Stock = Stock + 1 where ProductId = {product.Id} and LocationId = {orderOfInterest.LocationId}");
                _context.SaveChanges();

                OrderView orderView = new OrderView()
                {
                    TotalPrice = totalPrice,
                    OrderItems = OrderView.GetOrderItemViews(order.OrderId),
                    actionSucceeded = true
                };

                return orderView;
            }
            else
            {
                OrderView orderView = new OrderView();
                orderView.actionSucceeded = false;
                orderView.TotalPrice = orderOfInterest.TotalPrice;
                orderView.OrderItems = OrderView.GetOrderItemViews(order.OrderId);
                if (order.Action.Equals("remove")) orderView.message = "Can't remove an item you don't have in your cart.";
                else orderView.message = "That item is out of stock.";
                return orderView;
            }
        }
        

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public OrderView PostOrder(OrderInput order)
        {
            if (LocationInventory.itemIsAvailable(order.LocationId, order.ProductId) && order.Action.Equals("add"))
            {
                Guid orderID = Guid.NewGuid();
                Product product = _context.Products.FromSqlRaw<Product>($"select * from Products where Id = {order.ProductId}").First();
                _context.Database.ExecuteSqlRaw($"insert into Orders values ('{orderID}','{Order.cartOrderDate}',{order.CustomerId},{order.LocationId},{product.ProductPrice})");
                _context.Database.ExecuteSqlRaw($"insert into OrderItems (OrderId,ProductId) values ('{orderID}',{product.Id})");
                _context.Database.ExecuteSqlRaw($"update LocationInventory set Stock = Stock - 1 where ProductId = {product.Id} and LocationId = {order.LocationId}");
                _context.SaveChanges();
                OrderView orderView = new OrderView
                {
                    Id = orderID,
                    TotalPrice = product.ProductPrice,
                    OrderItems = new List<OrderItemView> { new OrderItemView() { Name1 = product.Name1, Quantity = 1 } },
                    actionSucceeded = true
                };

                return orderView;
            }
            else 
            {
                OrderView orderView = new OrderView();
                orderView.actionSucceeded = false;
                if (order.Action.Equals("remove")) orderView.message = "Can't remove an item you don't have in your cart.";
                else orderView.message = "That item is out of stock.";
                return orderView;
            }
            //_context.Database.ExecuteSqlRaw($"insert into Orders values ('{orderID}',getdate(),{order.CustomerId},{order.LocationId},{order.TotalPrice})");
            //string orderItemsInsert = $"insert into OrderItems (OrderId,ProductId) values ";
            //string locationInventoryUpdate = "";
            //FormattableString data;
            //foreach (OrderItem orderItem in order.OrderItems)
            //{ 
            //    data = $"('{orderID}',{orderItem.ProductId}),";
            //    orderItemsInsert += data.ToString();
            //    data = $"update LocationInventory set Stock = Stock - 1 where ProductId = {orderItem.ProductId} and LocationId = {order.LocationId} ";
            //    locationInventoryUpdate += data.ToString();  
            //}
            //int lastComma = orderItemsInsert.Length - 1;
            //orderItemsInsert = orderItemsInsert.Remove(lastComma);
            //_context.Database.ExecuteSqlRaw(orderItemsInsert);
            //_context.Database.ExecuteSqlRaw(locationInventoryUpdate);
            //_context.SaveChanges();
            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateException)
            //{
            //    if (OrderExists(order.Id))
            //    {
            //        return Conflict();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public void DeleteOrder(Guid id)
        {
            _context.Database.ExecuteSqlRaw($"delete from Orders where Id = '{id}'");
            _context.SaveChanges();
        }

        [HttpDelete]
        public void DeleteOrder()
        {
            _context.Database.ExecuteSqlRaw($"delete from Orders where OrderDate = '{Order.cartOrderDate}'");
            _context.SaveChanges();
        }

        private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }

        public Boolean itemIsInCart(Guid orderId, int productId) 
        {
            List<OrderItem> orderItems = _context.OrderItems.FromSqlRaw<OrderItem>($"select * from OrderItems where OrderId = '{orderId}' and ProductId = {productId}").ToList();
            if (orderItems.Count == 0) return false;
            else return true;
        }
    }
}
