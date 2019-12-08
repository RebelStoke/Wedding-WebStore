using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WeddingApp.Entity;

namespace WeddingApp.Infrastructure.SQLData
{
    public class DBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DBContext(DbContextOptions<DBContext> opt) : base(opt) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>().HasOne(c => c.customer).WithMany(o => o.allOrders).OnDelete(DeleteBehavior.SetNull);
}
    }
}
