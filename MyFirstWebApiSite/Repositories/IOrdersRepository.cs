using Entities;

namespace Repositories
{
    public interface IOrdersRepository
    {
        Task<Order> PostOrders(Order order);
    }
}