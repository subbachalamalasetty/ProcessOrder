namespace ProcessOrder.Infrastructure.DataContext
{
    public interface IProcessOrderContextFactory
    {
        ReadOnlyProcessOrderContext CreateReadOnlyContext();
        ProcessOrderDbContext CreateWriteableContext();
    }
}