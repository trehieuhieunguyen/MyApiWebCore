using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApiWebCore.Data;
using MyApiWebCore.Models;
using MyApiWebCore.Repositories.IRepository;
using System.Data;

namespace MyApiWebCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.Admin)]
    public class OrderItemController : ControllerBase
    {
        private IOrderItemRepository orderItemRepository;

        public OrderItemController(IOrderItemRepository orderItemRepository)
        {
            this.orderItemRepository = orderItemRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrderItem()
        {
            try
            {
                return Ok(await orderItemRepository.GetAllOrderDetailAsync());
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
                var newOrderItem = await orderItemRepository.AddOrderItemsAsyn(orderItemModel);
                var ordersItem = await orderItemRepository.GetOrderDetailAsyn(newOrderItem);
                return ordersItem == null ? NotFound() : Ok(ordersItem);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
