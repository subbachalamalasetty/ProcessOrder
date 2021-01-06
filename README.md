# ProcessOrder
Process an order placed by a user. Order is for a single product.

ProcessOrder.API will have Views and Controllers (not implemented) that uses logic from ProcessOrder.cs. In this task, it creates a UserOrder with one product and user details and calls ProcessOrderService.

ProcessOrder.Core has all the services. For email services, configuration is not set up and no real emails will be sent. In payment gateway services, we assume that 3rd party payment service verified the credit card info and was successful.

ProcessOrder.Infrastructure has Data Context, Database models, and repositories.
