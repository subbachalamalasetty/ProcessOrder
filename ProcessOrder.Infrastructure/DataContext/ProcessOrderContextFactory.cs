using Microsoft.EntityFrameworkCore;

namespace ProcessOrder.Infrastructure.DataContext
{
    public class ProcessOrderContextFactory : IProcessOrderContextFactory
    {
        public ReadOnlyProcessOrderContext CreateReadOnlyContext()
        {
            var readOnlyProcessOrderContext = new ReadOnlyProcessOrderContext();
            readOnlyProcessOrderContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return readOnlyProcessOrderContext;
        }

        public ProcessOrderDbContext CreateWriteableContext()
        {
            return new ProcessOrderDbContext();
        }
    }
}
