using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class OrderRepositoryIntegrationTest : IClassFixture<DatabaseFixture>
    {
        private readonly AdoNetUsers326077351Context _dbContext;
        private readonly OrdersRepository _orderRepository;

        public OrderRepositoryIntegrationTest(DatabaseFixture databaseFixture)
        {
            _dbContext = databaseFixture.Context;
            _orderRepository = new OrdersRepository(_dbContext);
        }

        [Fact]
        public async Task CreateOrder_ValidOrder_ReturnsOrder()
        {
            // Arrange
            var user = new User { Firstname = "Test", Lastname = "User", Email = "testuser@example.com", Password = "password" };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                OrderSum = 200.00,
                UserId = user.UserId
            };

            // Act
            var result = await _orderRepository.PostOrders(order);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(order.OrderDate, result.OrderDate);
            Assert.Equal(order.OrderSum, result.OrderSum);
            Assert.Equal(order.UserId, result.UserId);
        }
    }
}
