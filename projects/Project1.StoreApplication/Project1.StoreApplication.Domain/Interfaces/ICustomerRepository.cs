﻿using Project1.StoreApplication.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.StoreApplication.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        List<Customer> FindCustomer(string firstName, string lastName);
        void AddCustomer(Customer customer);
    }
}