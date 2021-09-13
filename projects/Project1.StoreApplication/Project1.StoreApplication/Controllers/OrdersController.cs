using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.StoreApplication.Models;
using Project1.StoreApplication.ViewModels;

namespace Project1.StoreApplication.Controllers
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
        [HttpGet("userType={userType}&id={id}")]
        public async Task<ActionResult<IEnumerable<OrderView>>> GetOrders(string userType, int id)
        {
            List<Order> orders = new List<Order>();
            List<OrderItem> orderItems = new List<OrderItem>();
            List<Customer> customers = new List<Customer>();
            List<Location> locations = new List<Location>();
            List<string> orderIdList = new List<string>();
            List<Product> products = new List<Product>();
            List<OrderView> orderViews = new List<OrderView>();
            List<OrderItemView> orderItemViews = new List<OrderItemView>();
           // if (userType.Equals("customer"))
            //{
                List<int> locationIdList = new List<int>();
                orders = _context.Orders.FromSqlRaw<Order>($"select * from Orders where CustomerID = {id}").ToList();
                foreach (Order order in orders)
                { 
                    if (!locationIdList.Contains(order.LocationId))
                        locationIdList.Add(order.LocationId);
                    orderIdList.Add(order.Id.ToString());
                }
                string locationIdListString = string.Join<int>(",", locationIdList);
                string orderIdListString = '\''+ string.Join<string>("','", orderIdList) + '\'';
                orderItems = _context.OrderItems.FromSqlRaw<OrderItem>($"select * from OrderItems where OrderId in ({orderIdListString})").ToList();
                customers = _context.Customers.FromSqlRaw<Customer>($"select * from Customers where Id = {id}").ToList();
                locations = _context.Locations.FromSqlRaw<Location>($"select * from Locations where Id in ({locationIdListString})").ToList();
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
                            orderView.CustomerName = customer.FirstName + customer.LastName;
                    foreach (Location location in locations)
                        if (orderView.LocationId == location.Id)
                            orderView.LocationName = location.CityName; 
                }
                return orderViews;
            //}
            //else return 
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

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(Guid id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public void PostOrder(Order order)
        {
            Guid orderID = Guid.NewGuid();
            _context.Database.ExecuteSqlRaw($"insert into Orders values ('{orderID}',getdate(),{order.CustomerId},{order.LocationId},{order.TotalPrice})");
            string orderItemsInsert = $"insert into OrderItems (OrderId,ProductId) values ";
            string locationInventoryUpdate = "";
            FormattableString data;
            foreach (OrderItem orderItem in order.OrderItems)
            { 
                data = $"('{orderID}',{orderItem.ProductId}),";
                orderItemsInsert += data.ToString();
                data = $"update LocationInventory set Stock = Stock - 1 where ProductId = {orderItem.ProductId} and LocationId = {order.LocationId} ";
                locationInventoryUpdate += data.ToString();  
            }
            int lastComma = orderItemsInsert.Length - 1;
            orderItemsInsert = orderItemsInsert.Remove(lastComma);
            _context.Database.ExecuteSqlRaw(orderItemsInsert);
            _context.Database.ExecuteSqlRaw(locationInventoryUpdate);
            _context.SaveChanges();
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
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
