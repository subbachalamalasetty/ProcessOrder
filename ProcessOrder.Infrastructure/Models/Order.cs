using System;

namespace ProcessOrder.Infrastructure.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal Amount { get; set; }

        public Product Product { get; set; }
    }
}