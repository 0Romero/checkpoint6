using cp6.Controllers;
using cp6.Models;
using cp6.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Test_Project
{
    public class UnitTest1
    {
        private readonly Mock<IInventoryItemRepository> _mockRepo;
        private readonly InventoryItemsController _controller;

        // Construtor renomeado corretamente
        public UnitTest1()
        {
            _mockRepo = new Mock<IInventoryItemRepository>();
            _controller = new InventoryItemsController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithListOfInventoryItems()
        {
            // Arrange
            var inventoryItems = new List<InventoryItem>
            {
                new InventoryItem { Id = "1", Name = "InventoryItem1", Price = 10.0M },
                new InventoryItem { Id = "2", Name = "InventoryItem2", Price = 20.0M }
            };

            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(inventoryItems);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnInventoryItems = Assert.IsType<List<InventoryItem>>(okResult.Value);
            Assert.Equal(2, returnInventoryItems.Count);
        }
    }
}
