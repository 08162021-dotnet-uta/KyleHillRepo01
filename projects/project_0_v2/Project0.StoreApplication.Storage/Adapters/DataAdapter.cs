using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Project0.StoreApplication.Domain.Models;
using System.Data.Entity;

namespace Project0.StoreApplication.Storage.Adapters
{
    public class DataAdapter : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Store> Store { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=StoreApplicationDB;Trusted_Connection=True");

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var config = modelBuilder.Entity<OAuthMembership>();
            config.ToTable("webpages_OAuthMembership");
        }

    }
    
}