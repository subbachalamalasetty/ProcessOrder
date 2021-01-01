using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Language.Flow;
using ProcessOrder.Core;
using ProcessOrder.Infrastructure;
using System;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcessOrder.Infrastructure.DataContext;
using ProcessOrder.Infrastructure.DataContext.TestConfiguration;
using ProcessOrder.Infrastructure.Models;

namespace ProcessOrder.Tests.Services
{
    [TestClass]
    public class ProcessOrderServiceTests
    {
        private ProcessOrderService _processOrderService;
        private Mock<IInventoryRepository> _inventoryRepositoryMock;
        private Mock<IOrderRepository> _orderRepositoryMock;
        private Mock<IEmailService> _emailServiceMock;
        private Mock<IPaymentGatewayService> _paymentGatewayMock;

        [TestInitialize]
        public void Initialize()
        {
            _inventoryRepositoryMock = new Mock<IInventoryRepository>();
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _emailServiceMock = new Mock<IEmailService>();
            _paymentGatewayMock = new Mock<IPaymentGatewayService>();

            _processOrderService = new ProcessOrderService(
                _inventoryRepositoryMock.Object, _orderRepositoryMock.Object,
                _emailServiceMock.Object, _paymentGatewayMock.Object);
        }

        [TestMethod]
        public async Task ProcessOrder_productIsAvailableInInventory_paymentChargeIsSuccessful_returnsTrue()
        {
            // Arrange
            var inventory = new Inventory
            {
                InventoryId = 1,
                ProductId = "testproduct",
                Quantity = 5
            };

            var product = new Product
            {
                ProductId = "testproduct"
            };

            var newOrder = new Order
            {
                OrderDate = new DateTime(2020, 12, 22),
                Amount = 34,
                Product = product
            };

            var userOrder = new UserOrder
            {
                UserName = "Bob",
                UserAddress = "addr-1, test-city, test-state-99999",
                OrderByUser = newOrder
            };

            // Mock repositories to get data
            _inventoryRepositoryMock
                .Setup(d => d.GetByProductId("testproduct"))
                .ReturnsAsync(inventory);

            _paymentGatewayMock
                .Setup(d => d.ChargePayment("1122", 34))
                .Returns(true);
            
            // Act
            var result = await _processOrderService.Process(userOrder, 3, "1122");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task ProcessOrder_productIsNotAvailableInInventory_paymentChargeIsSuccessful_returnsFalse()
        {
            // Arrange
            var inventory = new Inventory
            {
                InventoryId = 1,
                ProductId = "testproduct",
                Quantity = 1
            };

            var product = new Product
            {
                ProductId = "testproduct"
            };

            var newOrder = new Order
            {
                OrderDate = new DateTime(2020, 12, 22),
                Amount = 34,
                Product = product
            };

            var userOrder = new UserOrder
            {
                UserName = "Bob",
                UserAddress = "addr-1, test-city, test-state-99999",
                OrderByUser = newOrder
            };

            // Mock repositories to get data
            _inventoryRepositoryMock
                .Setup(d => d.GetByProductId("testproduct"))
                .ReturnsAsync(inventory);

            _paymentGatewayMock
                .Setup(d => d.ChargePayment("1122", 34))
                .Returns(true);

            // Act
            var result = await _processOrderService.Process(userOrder, 3, "1122");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task ProcessOrder_productIsAvailableInInventory_paymentChargeIsNotSuccessful_returnsFalse()
        {
            // Arrange
            var inventory = new Inventory
            {
                InventoryId = 1,
                ProductId = "testproduct",
                Quantity = 1
            };

            var product = new Product
            {
                ProductId = "testproduct"
            };

            var newOrder = new Order
            {
                OrderDate = new DateTime(2020, 12, 22),
                Amount = 34,
                Product = product
            };

            var userOrder = new UserOrder
            {
                UserName = "Bob",
                UserAddress = "addr-1, test-city, test-state-99999",
                OrderByUser = newOrder
            };

            // Mock repositories to get data
            _inventoryRepositoryMock
                .Setup(d => d.GetByProductId("testproduct"))
                .ReturnsAsync(inventory);

            _paymentGatewayMock
                .Setup(d => d.ChargePayment("1122", 34))
                .Returns(false);

            // Act
            var result = await _processOrderService.Process(userOrder, 3, "1122");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task ProcessOrder_productIsAvailableInInventory_paymentChargeIsSuccessful_sendsEmail()
        {
            // Arrange
            var inventory = new Inventory
            {
                InventoryId = 1,
                ProductId = "testproduct",
                Quantity = 5
            };

            var product = new Product
            {
                ProductId = "testproduct"
            };

            var newOrder = new Order
            {
                OrderDate = new DateTime(2020, 12, 22),
                Amount = 34,
                Product = product
            };

            var userOrder = new UserOrder
            {
                UserName = "Bob",
                UserAddress = "addr-1, test-city, test-state-99999",
                OrderByUser = newOrder
            };

            // Mock repositories to get data
            _inventoryRepositoryMock
                .Setup(d => d.GetByProductId("testproduct"))
                .ReturnsAsync(inventory);

            _paymentGatewayMock
                .Setup(d => d.ChargePayment("1122", 34))
                .Returns(true);

            // Act
            var result = await _processOrderService.Process(userOrder, 3, "1122");

            // Assert
            _emailServiceMock.Verify(v =>
                v.SendEmail(It.Is<MailMessage>(mm => mm.To[0].Address == "shippingDept@testcompany.com")));
        }

        
    }
}
