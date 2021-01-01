using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProcessOrder.Infrastructure.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string ProductId { get; set; }

        public decimal Price { get; set; }

        public virtual IList<Order> Orders { get; set; }
    }
}