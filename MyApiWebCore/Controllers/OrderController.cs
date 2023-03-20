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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderAsync(int id)
        {
            try
            {
                await orderRe.DeleteOrderAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderAsync(int id,OrderModel orderModel)
        {
            try
            {
                await orderRe.UpdateOrderAsync(id,orderModel);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
