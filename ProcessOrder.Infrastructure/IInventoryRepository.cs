using ProcessOrder.Infrastructure.Models;
using System.Threading.Tasks;

namespace ProcessOrder.Infrastructure
{
    public interface IInventoryRepository
    {
        Task<Inventory> GetByProductId(string productId);
    }
}