using Microsoft.EntityFrameworkCore;
using System;

namespace ProcessOrder.Infrastructure.DataContext
{
    public class ReadOnlyProcessOrderContext : ProcessOrderDbContext
    {
        public ReadOnlyProcessOrderContext()
        {
        }

        public ReadOnlyProcessOrderContext(DbContextOptions<ProcessOrderDbContext> options) : base(options)
        {
        }

        public override int SaveChanges()
        {
            throw new InvalidOperationException("This context is read-only.");
        }
    }
}
