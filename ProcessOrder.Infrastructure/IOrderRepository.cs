using ProcessOrder.Infrastructure.Models;
using System.Threading.Tasks;

namespace ProcessOrder.Infrastructure
{
    public interface IOrderRepository
    {
        Task<Order> GetOrder(int orderId);
        Task<int> AddOrder(Order order);
    }
}