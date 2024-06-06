using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private AdoNetUsers326077351Context _OrdersContext;

        public OrdersRepository(AdoNetUsers326077351Context OrdersContext)
        {
            _OrdersContext = OrdersContext;
        }

        public async Task<Order> PostOrders(Order order)
        {
            await _OrdersContext.Orders.AddAsync(order);
            await _OrdersContext.SaveChangesAsync();
            return order;
        }
    }
}
