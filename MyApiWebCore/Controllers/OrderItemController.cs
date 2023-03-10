using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApiWebCore.Data;
using MyApiWebCore.Models;
using MyApiWebCore.Repositories;
using System.Data;

namespace MyApiWebCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.Admin)]
    public class OrderItemController : ControllerBase
    {
        private IOrderItemRepository orderRepository;

        public OrderItemController(IOrderItemRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrderItem()
        {
            try
            {
                return Ok(await orderRepository.GetAllOrderDetailAsync());
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddOrderItem(OrderItemModel orderItemModel)
        {
            try
            {
                var newOrderItem = await orderRepository.AddOrderItemsAsyn(orderItemModel);
                var ordersItem = await orderRepository.GetOrderDetailAsyn(newOrderItem);
                return ordersItem == null ? NotFound() : Ok(ordersItem);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
