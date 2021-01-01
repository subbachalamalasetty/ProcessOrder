using ProcessOrder.Core;
using ProcessOrder.Infrastructure.Models;
using System.Threading.Tasks;

namespace ProcessOrder.API
{
    public class ProcessOrder
    {
        private readonly IProcessOrderService _processOrderService;
        public ProcessOrder(IProcessOrderService processOrderService)
        {
            _processOrderService = processOrderService;
        }

        public async Task Process(string productId, decimal amount, int qty, string creditCardNumber)
        {
            var order = new Order
            {
                Amount = amount,
                Product = new Product
                {
                    ProductId = productId
                },
            };

            var userOrder = new UserOrder
            {
                UserName = "Bob",
                UserAddress = "addr-1, test-city, test-state-99999",
                OrderByUser = order
            };

            await _processOrderService.Process(userOrder, qty, creditCardNumber);
        }
    }
}