using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProcessOrder.Infrastructure.DataContext;
using ProcessOrder.Infrastructure.DataContext.TestConfiguration;
using System.Threading.Tasks;
using ProcessOrder.Infrastructure;
using ProcessOrder.Infrastructure.Models;

namespace ProcessOrder.Tests.Repositories
{
    [TestClass]
    public class InventoryRepositoryTests
    {
        private AutoMoqer _mocker;

        [TestInitialize]
        public void Initialize()
        {
            TestContextHelper.CleanDatabase();

            _mocker = new AutoMoqer();
            _mocker.SetInstance<IProcessOrderContextFactory>(new ProcessOrderContextFactoryForTest());
        }

        [TestMethod]
        public async Task GetByProductId_returnsInventory()
        {
            // Arrange
            TestContextHelper.AddEntity(new Inventory
            {
                InventoryId = 1,
                ProductId = "testproduct",
                Quantity = 5
            });

            // Use Mock up
            var inventoryRepository = _mocker.Resolve<InventoryRepository>();

            // Act
            var inventory = await inventoryRepository.GetByProductId("testproduct");

            // Assert
            Assert.AreEqual(1, inventory.InventoryId);
            Assert.AreEqual("testproduct", inventory.ProductId);
            Assert.AreEqual(5, inventory.Quantity);
        }
    }
}
