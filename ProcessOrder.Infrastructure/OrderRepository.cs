using Microsoft.EntityFrameworkCore;
using ProcessOrder.Infrastructure.DataContext;
using ProcessOrder.Infrastructure.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessOrder.Infrastructure
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IProcessOrderContextFactory _processOrderContextFactory;

        public OrderRepository(IProcessOrderContextFactory processOrderContextFactory)
        {
            _processOrderContextFactory = processOrderContextFactory;
        }

        public async Task<Order> GetOrder(int orderId)
        {
            return await _processOrderContextFactory.CreateReadOnlyContext()
                .Order.FirstOrDefaultAsync(x => x.OrderId == orderId);
        }

        public async Task<int> AddOrder(Order order)
        {
            var writeableContext = _processOrderContextFactory.CreateWriteableContext();

            await writeableContext.Order.AddAsync(order);

            await writeableContext.SaveChangesAsync(CancellationToken.None);

            return order.OrderId;
        }
    }
}