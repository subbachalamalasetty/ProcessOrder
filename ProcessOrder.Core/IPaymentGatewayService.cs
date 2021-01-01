namespace ProcessOrder.Core
{
    public interface IPaymentGatewayService
    {
        bool ChargePayment(string creditCardNumber, decimal amount);
    }
}