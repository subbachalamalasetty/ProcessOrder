using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;

namespace ProcessOrder.Infrastructure.DataContext.TestConfiguration
{
    public class ProcessOrderContextFactoryForTest : IProcessOrderContextFactory
    {
        private static readonly InMemoryDatabaseRoot InMemoryDatabaseRoot;

        static ProcessOrderContextFactoryForTest()
        {
            InMemoryDatabaseRoot = new InMemoryDatabaseRoot();
        }

        public ReadOnlyProcessOrderContext CreateReadOnlyContext()
        {
            return CreateReadOnlyProcessOrderContext();
        }

        public ProcessOrderDbContext CreateWriteableContext()
        {
            return CreateProcessOrderContext();
        }

        private static ProcessOrderDbContext CreateProcessOrderContext()
        {
            var options = new DbContextOptionsBuilder<ProcessOrderDbContext>()
                .UseInMemoryDatabase("ProcessOrder", InMemoryDatabaseRoot)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            var processOrderContext = new ProcessOrderDbContext(options);

            processOrderContext.SetAllValueGeneratorsAsResettable();

            return processOrderContext;
        }

        private static ReadOnlyProcessOrderContext CreateReadOnlyProcessOrderContext()
        {
            var options = new DbContextOptionsBuilder<ProcessOrderDbContext>()
                .UseInMemoryDatabase("ProcessOrder", InMemoryDatabaseRoot)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            var processOrderContext = new ReadOnlyProcessOrderContext(options);

            processOrderContext.SetAllValueGeneratorsAsResettable();

            return processOrderContext;
        }

    }
}