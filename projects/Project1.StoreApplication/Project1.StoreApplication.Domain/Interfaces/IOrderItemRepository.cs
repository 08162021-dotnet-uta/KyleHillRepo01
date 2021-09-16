using Project1.StoreApplication.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.StoreApplication.Domain.Interfaces
{
    public interface IOrderItemRepository
    {
        List<OrderItem> GetAllOrderItems();
    }
}
