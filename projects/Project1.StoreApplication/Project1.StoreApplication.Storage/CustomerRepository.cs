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
    public class CustomerRepository : ICustomerRepository
    {

        private readonly Kyles_Pizza_ShopContext _context;

        public CustomerRepository(Kyles_Pizza_ShopContext context)
        { _context = context; }
        public List<Customer> FindCustomer(string firstName, string lastName) 
        { 
            return _context.Customers.FromSqlRaw<Customer>($"select * from Customers where FirstName = '{firstName}' and LastName = '{lastName}'").ToList();
        }
        public void AddCustomer(Customer customer)
        {
            _context.Database.ExecuteSqlRaw($"insert into Customers (FirstName,LastName) values ('{customer.FirstName}','{customer.LastName}')");
            _context.SaveChanges();
        }
    }
}
