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

        public DBContext(DbContextOptions<DBContext> opt) : base(opt)
        {
        }
    }
}
