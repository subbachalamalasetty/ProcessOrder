using ProcessOrder.Infrastructure;
using ProcessOrder.Infrastructure.Models;
using System;
using System.Threading.Tasks;
using System.Net.Mail;

namespace ProcessOrder.Core
{
    public class ProcessOrderService : IProcessOrderService
    {
        private const int PaymentFee = 2;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailService _emailService;
        private readonly IPaymentGatewayService _paymentGatewayService;
        
        public ProcessOrderService(IInventoryRepository inventoryRepository,
            IOrderRepository orderRepository,
            IEmailService emailService,
            IPaymentGatewayService paymentGatewayService)
        {
            _inventoryRepository = inventoryRepository;
            _orderRepository = orderRepository;
            _emailService = emailService;
            _paymentGatewayService = paymentGatewayService;
        }

        /// <summary>
        /// This method will take UserOrder information, product quantity ordered (it will be one in this scenario)
        /// and credit card number. It checks if product is available in inventory and if yes,
        /// adds the payment fees to the total amount and charges the credit card.
        /// If payment is successful, then notify the shipping department about the order
        /// If all of the above are successful, it returns true
        /// </summary>
        /// <param name="userOrder"></param>
        /// <param name="qty"></param>
        /// <param name="creditCardNumber"></param>
        /// <returns>true or false</returns>
        public async Task<bool> Process(UserOrder userOrder, int qty, string creditCardNumber)
        {
            var isProcessed = false;

            // Check if product is available in inventory
            var isProductAvailable = await CheckInventory(userOrder.OrderByUser.Product.ProductId, qty);
            
            if (isProductAvailable)
            {
                try
                {
                    userOrder.OrderByUser.Amount = GetTotalAmountForOrder(creditCardNumber, userOrder.OrderByUser.Amount);

                    if (userOrder.OrderByUser.Amount > 0)
                    {
                        //Add the Order (with fees charged) in the database
                        var orderId = _orderRepository.AddOrder(userOrder.OrderByUser);

                        NotifyOrder(userOrder);

                        isProcessed = true;
                    }
                }
                catch (Exception ex)
                {
                    //  send the email to support about failed order
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("userOrder@testcompany.com"),
                        Subject = "Order Processing Failed",
                        Body = string.Format("Order Processing failed with exception : {0}", ex.ToString()),
                        To = { new MailAddress("support@testcompany.com") },
                        IsBodyHtml = true
                    };

                    _emailService.SendEmail(mailMessage);
                }
            }

            return isProcessed;
        }

        /// <summary>
        /// Checks if product is availabe in the inventory. In this scenario, only 1 product
        /// quantity will be requested, there should be 1 or more products available.
        /// It returns false if not available and also returns false user requested quantity is 0.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="qty"></param>
        /// <returns>true or false</returns>
        private async Task<bool> CheckInventory(string productId, int qty)
        {
            var inventory = await _inventoryRepository.GetByProductId(productId);

            if (inventory == null || qty == 0)
                return false;

            return inventory.Quantity >= qty;
        }

        /// <summary>
        /// Checks if credit card charge payment is successful and if successful
        /// then payment fees is added to the order amount and returns the total amount
        /// If not successful, it reurns 0
        /// </summary>
        /// <param name="creditCardNumber"></param>
        /// <param name="amount"></param>
        /// <returns>Total amount with added payment fees</returns>
        private decimal GetTotalAmountForOrder(string creditCardNumber, decimal amount)
        {
            // charge credit card                     
            if (_paymentGatewayService.ChargePayment(creditCardNumber, amount))
            {
                amount += PaymentFee;
                return amount;
            }

            return 0;
        }

        /// <summary>
        /// Notify the shipping deparment that an order was placed. 
        /// </summary>
        /// <param name="order"></param>
        private void NotifyOrder(UserOrder userOrder)
        {
            var emailContent =
                string.Format("Order of OrderID : {0} has been placed for {1} and ready to ship to this address {2} ",
                    userOrder.OrderByUser.OrderId, userOrder.UserName, userOrder.UserAddress);

            var subject = string.Format("Order of OrderID : {0} Successfully processed", userOrder.OrderByUser.OrderId);

            var mailMessage = new MailMessage
            {
                From = new MailAddress("userOrder@testcompany.com"),
                Subject = subject,
                Body = emailContent,
                To = { new MailAddress("shippingDept@testcompany.com") },
                IsBodyHtml = true
            };

            _emailService.SendEmail(mailMessage);
        }
    }
}