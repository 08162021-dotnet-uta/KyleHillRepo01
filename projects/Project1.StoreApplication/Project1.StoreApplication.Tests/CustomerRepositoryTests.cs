using Microsoft.EntityFrameworkCore;
using Project1.StoreApplication.Domain.Models;
using Project1.StoreApplication.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Project1.StoreApplication.Tests
{
    public class CustomerRepositoryTests
    {
        private readonly Kyles_Pizza_ShopContext _context = new Kyles_Pizza_ShopContext();
        
        [Fact]
        public void FindCustomerTest()
        {
            _context.Database.ExecuteSqlRaw("delete from Customers");
            _context.SaveChanges();
            string firstName = "Jason";
            string lastName = "Ellis";
            _context.Customers.Add(new Customer() { FirstName = firstName, LastName = lastName});
            _context.SaveChanges();

            CustomerRepository customerRepository = new CustomerRepository(_context);
            List<Customer> customer = customerRepository.FindCustomer(firstName,lastName);
            Assert.Equal(firstName, customer[0].FirstName);
            Assert.Equal(lastName, customer[0].LastName);
        }

        [Fact]
        public void AddCustomerTest()
        {
            Customer customer = new Customer() { FirstName = "Jason", LastName = "Ellis" };
            CustomerRepository customerRepository = new CustomerRepository(_context);
            int actualId = customerRepository.AddCustomer(customer);

            Customer customer1 = _context.Customers.FromSqlRaw($"select * from Customers where FirstName = '{customer.FirstName}' and LastName = '{customer.LastName}'").First();
            int expectedId = customer1.Id;
            Assert.Equal(expectedId, actualId);
        }


        public int AddCustomer(Customer customer)
        {
            _context.Database.ExecuteSqlRaw($"insert into Customers (FirstName,LastName) values ('{customer.FirstName}','{customer.LastName}')");
            _context.SaveChanges();
            Customer customer1 = _context.Customers.FromSqlRaw($"select * from Customers where FirstName = '{customer.FirstName}' and LastName = '{customer.LastName}'").First();
            return customer1.Id;
        }

    }
}
