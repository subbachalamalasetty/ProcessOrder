using Microsoft.EntityFrameworkCore;
using ProcessOrder.Infrastructure.DataContext;
using ProcessOrder.Infrastructure.Models;
using System.Threading.Tasks;

namespace ProcessOrder.Infrastructure
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IProcessOrderContextFactory _processOrderContextFactory;

        public InventoryRepository(IProcessOrderContextFactory processOrderContextFactory)
        {
            _processOrderContextFactory = processOrderContextFactory;
        }

        public async Task<Inventory> GetByProductId(string productId)
        {
            return await _processOrderContextFactory.CreateReadOnlyContext()
                .Inventory.FirstOrDefaultAsync(x => x.ProductId == productId);
        }
    }
}