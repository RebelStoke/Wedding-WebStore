using Microsoft.EntityFrameworkCore;
using WeddingApp.Entity;

namespace WeddingApp.Infrastructure.SQLData
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> opt) : base(opt)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        
        public DbSet<DateObject> Dates { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .HasOne(c => c.Customer)
                .WithMany(o => o.AllOrders)
                .OnDelete(DeleteBehavior.SetNull);
            
        }
    }
}