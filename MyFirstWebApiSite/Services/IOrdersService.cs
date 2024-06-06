using Entities;

namespace Services
{
    public interface IOrdersService
    {
        Task<Order> PostOrders(Order order);
    }
}