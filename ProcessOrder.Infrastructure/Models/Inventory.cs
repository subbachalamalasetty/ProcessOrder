using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProcessOrder.Infrastructure.Models
{
    public class Inventory
    {
        [Key]
        public int InventoryId { get; set; }
        public int Id { get; set; }
        public string ProductId { get; set; }

        [ForeignKey("Id")]
        public virtual Product Product { get; set; }

        public int Quantity { get; set; }
    }
}