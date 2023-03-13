using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyApiWebCore.Data;
using MyApiWebCore.Models;
using MyApiWebCore.Models.ModelVM;
using MyApiWebCore.Repositories;
using MyApiWebCore.Repositories.IRepository;

namespace MyApiWebCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionProduct : ControllerBase
    {
        private ITransactionHistoryRepositrory historyRepositrory;

        public TransactionProduct(ITransactionHistoryRepositrory historyRepositrory) 
        {
            this.historyRepositrory = historyRepositrory;
        }

        [HttpPost]
        public async Task<IActionResult> TransactionProductAsync(List<OrderItemModel> orderDetails)
        {
            if (orderDetails == null)
            {
                return NotFound();
            }
            else
            {
                var orders = await historyRepositrory.TransactionHistory(orderDetails);
                return Ok(orders);
            }
        }
    }
}
