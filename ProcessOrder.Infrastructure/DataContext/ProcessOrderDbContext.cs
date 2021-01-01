using System.Configuration;
using Microsoft.EntityFrameworkCore;
using ProcessOrder.Infrastructure.Models;

namespace ProcessOrder.Infrastructure.DataContext
{
    public class ProcessOrderDbContext : DbContext
    {
        public ProcessOrderDbContext()
        {
        }

        public ProcessOrderDbContext(DbContextOptions<ProcessOrderDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Product { get; set; }

        public virtual DbSet<Inventory> Inventory { get; set; }

        public virtual DbSet<Order> Order { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //        optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["ProcessOrderConnectionString"].ConnectionString);
        //}
    }
}
