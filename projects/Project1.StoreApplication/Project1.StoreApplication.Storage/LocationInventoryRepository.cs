﻿using Microsoft.EntityFrameworkCore;
using Project1.StoreApplication.Domain.Interfaces;
using Project1.StoreApplication.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.StoreApplication.Storage
{
    
    public class LocationInventoryRepository : ILocationInventoryRepository
    {
        private readonly Kyles_Pizza_ShopContext _context;

        public LocationInventoryRepository(Kyles_Pizza_ShopContext context)
        { _context = context; }

        public IEnumerable<LocationInventory> GetLocationInventory(int LocationId)
        {
            return _context.LocationInventories.FromSqlRaw<LocationInventory>($"select * from LocationInventory where LocationId = {LocationId} order by ProductId").ToList();
        }    
}
}
