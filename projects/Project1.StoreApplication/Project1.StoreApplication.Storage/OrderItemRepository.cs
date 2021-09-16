using Microsoft.EntityFrameworkCore;
using Project1.StoreApplication.Domain.Interfaces;
using Project1.StoreApplication.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.StoreApplication.Storage
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly Kyles_Pizza_ShopContext _context;
        public OrderItemRepository(Kyles_Pizza_ShopContext context)
        { _context = context; }
        public List<OrderItem> GetAllOrderItems()
        {
            return _context.OrderItems.FromSqlRaw<OrderItem>($"select * from OrderItems").ToList();
        }
    }
}
