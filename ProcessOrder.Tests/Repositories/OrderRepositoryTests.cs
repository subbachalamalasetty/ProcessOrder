using System;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProcessOrder.Infrastructure.DataContext;
using System.Threading.Tasks;
using ProcessOrder.Infrastructure;
using ProcessOrder.Infrastructure.Models;
using ProcessOrder.Infrastructure.DataContext.TestConfiguration;
using System.Linq;

namespace ProcessOrder.Tests.Repositories
{
    [TestClass]
    public class OrderRepositoryTests
    {
        private ProcessOrderDbContext _context;
        private ProcessOrderContextFactoryForTest _contextFactoryForTest;
        private OrderRepository _orderRepository;

        [TestInitialize]
        public void Initialize()
        {
            TestContextHelper.CleanDatabase();

            _contextFactoryForTest = new ProcessOrderContextFactoryForTest();

            _context = _contextFactoryForTest.CreateWriteableContext();

            _orderRepository = new OrderRepository(_contextFactoryForTest);
        }
        
        [TestMethod]
        public async Task AddOrder_addsNewOrder()
        {
            // Arrange
            TestContextHelper.AddEntity(new Order
            {
                OrderId = 1
            });

            var orderToAdd = new Order
            {
                OrderId = 2,
                OrderDate = new DateTime(2020, 12, 22),
                Amount = 34
            };

            // Act
            var newOrderId = await _orderRepository.AddOrder(orderToAdd);

            // Get data from Test context
            var allOrders = _contextFactoryForTest.CreateReadOnlyContext().Order.ToList();
            var addedOrder = allOrders.Where(x => x.OrderId == 2).FirstOrDefault();

            // Assert
            Assert.AreEqual(allOrders.Count, 2);
            Assert.AreEqual(addedOrder.OrderId, 2);
            Assert.AreEqual(addedOrder.OrderDate, new DateTime(2020, 12, 22));
            Assert.AreEqual(addedOrder.Amount, 34);
        }

    }
}
