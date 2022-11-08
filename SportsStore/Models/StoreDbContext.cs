using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace SportsStore.Models
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products => this.Set<Product>();

        public DbSet<Order> Orders => this.Set<Order>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(message => Debug.WriteLine(message));
        }
    }
}