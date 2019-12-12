using Microsoft.EntityFrameworkCore;
using WeddingApp.Entity;

namespace WeddingApp.Infrastructure.SQLData
{
    public class DBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<DatesAssigned> Dates { get; set; }

        public DBContext(DbContextOptions<DBContext> opt) : base(opt)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .HasOne(c => c.Customer)
                .WithMany(o => o.AllOrders)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<DatesAssigned>()
                .HasOne<Order>(ad => ad.Order)
                .WithOne(s => s.DateForOrderToBeCompleted)
                .HasForeignKey<DatesAssigned>(ad => ad.ID);

            modelBuilder.Entity<DatesAssigned>()
                .HasAlternateKey(a => a.TakenDate);
        }
    }
}