using Entities;
using Microsoft.Extensions.Logging;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrdersService : IOrdersService
    {
        private IOrdersRepository _ordersRepository;
        private IProductsServices _productService;
        private readonly ILogger<OrdersService> _logger;

        public OrdersService(IOrdersRepository orderRepository, ILogger<OrdersService> logger, IProductsServices productService)
        {
            _ordersRepository = orderRepository;
            _productService = productService;
            _logger = logger;
        }

        private async Task<double> checkSum(Order order)
        {
            double order_sum = 0;

            foreach (OrderItem i in order.OrderItems)
            {
                Product product = await _productService.GetProductById(i.ProductId);
                order_sum += product.Price * i.Quantity;
            }
            return order_sum;
        }

        public async Task<Order> PostOrders(Order order)
        {
            double sum =await checkSum(order);
            if(sum!=order.OrderSum)
                _logger.LogError($"user {order.UserId}  tried perchasing with a difffrent price {order.OrderSum} instead of {sum}");

            order.OrderSum = sum;
            return await _ordersRepository.PostOrders(order);
         
        }
    }
}
