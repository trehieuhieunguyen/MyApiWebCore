using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApiWebCore.Models;
using MyApiWebCore.Repositories;

namespace MyApiWebCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderRepository orderRe;

        public OrderController(IOrderRepository orderRe) 
        {
            this.orderRe = orderRe;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderItem()
        {
            try
            {
                return Ok(await orderRe.GetAllOrderAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrderAsync(OrderModel orderModel)
        {
            try
            {
                var newOrderModel = await orderRe.AddOrderAsync(orderModel);
                var order = await orderRe.GetOrderByIdAsync(newOrderModel);
                return order == null ? NotFound() : Ok(order);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
