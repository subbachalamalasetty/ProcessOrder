namespace ProcessOrder.Core
{
    /// <summary>
    /// Call a 3rd party payment service to verify the credit card info.
    /// For simplicity assume incoming Order contains user's credit card number
    /// and amount for the order to be processed
    /// </summary>
    public class PaymentGatewayService : IPaymentGatewayService
    {
        /// <summary>
        /// This method returns true after verifying credit card info
        /// </summary>
        /// <param name="creditCardNumber"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool ChargePayment(string creditCardNumber, decimal amount)
        {
            return true;
        }
    }
}