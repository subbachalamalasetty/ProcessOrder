using ProcessOrder.Infrastructure.Models;
using System.Threading.Tasks;

namespace ProcessOrder.Core
{
    public interface IProcessOrderService
    {
        Task<bool> Process(UserOrder userOrder, int qty, string creditCardNumber);
    }
}