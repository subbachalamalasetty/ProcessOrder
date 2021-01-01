namespace ProcessOrder.Infrastructure.Models
{
    public class UserOrder
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserAddress { get; set; }
        public Order OrderByUser { get; set; }
    }
}