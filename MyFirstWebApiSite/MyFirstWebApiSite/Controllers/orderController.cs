using AutoMapper;
using DTOs;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace MyFirstWebApiSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class orderController : ControllerBase
    {
        private IOrdersService _orderService;
       
        private IMapper _mapper;

      
        public orderController(IOrdersService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<orderDTO>> PostOrders([FromBody] orderDTO ordersDTO)
        {
            Order orders = _mapper.Map<orderDTO, Order>(ordersDTO);
            Order newOrder = await _orderService.PostOrders(orders);
            orderDTO orderToReturn = _mapper.Map<Order, orderDTO>(newOrder);
            if (newOrder != null)
                return Ok(orderToReturn);
            return BadRequest();
        }
    }
}
