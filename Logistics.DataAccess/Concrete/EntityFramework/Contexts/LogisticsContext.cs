using Logistics.Entity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Logistics.DataAccess.Concrete
{
    public class LogisticsContext : DbContext
    {
        public LogisticsContext(DbContextOptions<LogisticsContext> options) : base(options) { }
        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable(nameof(Product), "Product");
            modelBuilder.Entity<Order>().ToTable(nameof(Order), "Order");
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
