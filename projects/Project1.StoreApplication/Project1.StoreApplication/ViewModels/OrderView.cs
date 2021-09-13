using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.StoreApplication.ViewModels
{
    public class OrderView
    {
        public OrderView()
        {
            OrderItems = new HashSet<OrderItemView>();
        }
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public decimal TotalPrice { get; set; }
        public virtual ICollection<OrderItemView> OrderItems { get; set; }

    }
}
